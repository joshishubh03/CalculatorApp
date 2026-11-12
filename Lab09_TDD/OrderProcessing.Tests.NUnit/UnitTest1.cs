using Moq;
using NUnit.Framework;
using OrderProcessing.Core.Abstractions;
using OrderProcessing.Core.Models;
using OrderProcessing.Core.Services;

namespace OrderProcessing.Tests.NUnit;

[TestFixture]
public sealed class OrderProcessingServiceTests
{
    private Mock<IInventoryService> _inventory = null!;
    private Mock<IPaymentService> _payment = null!;
    private Mock<IOrderRepository> _orders = null!;
    private Mock<IOrderNotificationService> _notifications = null!;

    [SetUp]
    public void SetUp()
    {
        _inventory = new Mock<IInventoryService>();
        _payment = new Mock<IPaymentService>();
        _orders = new Mock<IOrderRepository>();
        _notifications = new Mock<IOrderNotificationService>();
    }

    private OrderProcessingService CreateSut() =>
        new(_inventory.Object, _payment.Object,
            _orders.Object, _notifications.Object);

    [Test]
    public async Task PlaceOrderAsync_WhenAllStepsSucceed_ReturnsConfirmedOrder()
    {
        var request = new PlaceOrderRequest(
            "CUST-1",
            new[] { new OrderItem("BOOK", 2, 100m) },
            "payment-token");

        _inventory.Setup(x => x.ReserveAsync(
            "BOOK", 2, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _payment.Setup(x => x.ChargeAsync(
            "payment-token", 200m, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PaymentResult(true, "TX-100", null));

        _orders.Setup(x => x.SaveConfirmedAsync(
            It.IsAny<Guid>(), "CUST-1",
            It.IsAny<IReadOnlyList<OrderItem>>(), 200m, "TX-100",
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _notifications.Setup(x => x.SendConfirmationAsync(
            It.IsAny<Guid>(), "CUST-1", 200m,
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var sut = CreateSut();

        var result = await sut.PlaceOrderAsync(request);

        Assert.That(result.Total, Is.EqualTo(200m));
        Assert.That(result.Status, Is.EqualTo(OrderStatus.Confirmed));
        _payment.Verify(x => x.ChargeAsync(
            "payment-token", 200m, result.OrderId,
            It.IsAny<CancellationToken>()), Times.Once);
        _orders.Verify(x => x.SaveConfirmedAsync(
            result.OrderId, "CUST-1",
            It.IsAny<IReadOnlyList<OrderItem>>(), 200m, "TX-100",
            It.IsAny<CancellationToken>()), Times.Once);
        _notifications.Verify(x => x.SendConfirmationAsync(
            result.OrderId, "CUST-1", 200m,
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task PlaceOrderAsync_WhenStepsRunInOrder_InvokesDependenciesInSequence()
    {
        var request = new PlaceOrderRequest(
            "CUST-1",
            new[] { new OrderItem("BOOK", 2, 100m) },
            "payment-token");

        var calls = new List<string>();

        _inventory.Setup(x => x.ReserveAsync(
            "BOOK", 2, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Callback(() => calls.Add("reserve"))
            .ReturnsAsync(true);

        _payment.Setup(x => x.ChargeAsync(
            "payment-token", 200m, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Callback(() => calls.Add("charge"))
            .ReturnsAsync(new PaymentResult(true, "TX-100", null));

        _orders.Setup(x => x.SaveConfirmedAsync(
            It.IsAny<Guid>(), "CUST-1",
            It.IsAny<IReadOnlyList<OrderItem>>(), 200m, "TX-100",
            It.IsAny<CancellationToken>()))
            .Callback(() => calls.Add("save"))
            .Returns(Task.CompletedTask);

        _notifications.Setup(x => x.SendConfirmationAsync(
            It.IsAny<Guid>(), "CUST-1", 200m,
            It.IsAny<CancellationToken>()))
            .Callback(() => calls.Add("notify"))
            .Returns(Task.CompletedTask);

        var sut = CreateSut();

        await sut.PlaceOrderAsync(request);

        Assert.That(calls, Is.EqualTo(new[] { "reserve", "charge", "save", "notify" }));
    }

    [Test]
    public void PlaceOrderAsync_WhenInventoryUnavailable_DoesNotChargePayment()
    {
        var request = new PlaceOrderRequest(
            "CUST-2",
            new[] { new OrderItem("OUT-OF-STOCK", 1, 50m) },
            "token");

        _inventory.Setup(x => x.ReserveAsync(
            "OUT-OF-STOCK", 1, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var sut = CreateSut();

        Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.PlaceOrderAsync(request));

        _payment.Verify(x => x.ChargeAsync(
            It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public void PlaceOrderAsync_WhenPaymentDeclined_ReleasesReservation()
    {
        var request = new PlaceOrderRequest(
            "CUST-3", new[] { new OrderItem("BOOK", 2, 100m) }, "token");

        _inventory.Setup(x => x.ReserveAsync(
            "BOOK", 2, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _payment.Setup(x => x.ChargeAsync(
            "token", 200m, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PaymentResult(false, null, "Insufficient funds"));

        var sut = CreateSut();

        Assert.ThrowsAsync<PaymentDeclinedException>(async () => await sut.PlaceOrderAsync(request));

        _inventory.Verify(x => x.ReleaseAsync(
            "BOOK", 2, It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        _orders.Verify(x => x.SaveConfirmedAsync(
            It.IsAny<Guid>(), It.IsAny<string>(),
            It.IsAny<IReadOnlyList<OrderItem>>(), It.IsAny<decimal>(),
            It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        _notifications.Verify(x => x.SendConfirmationAsync(
            It.IsAny<Guid>(), It.IsAny<string>(),
            It.IsAny<decimal>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public void PlaceOrderAsync_WhenLaterItemCannotBeReserved_ReleasesEarlierReservation()
    {
        var request = new PlaceOrderRequest(
            "CUST-4",
            new[]
            {
                new OrderItem("P1", 1, 20m),
                new OrderItem("P2", 1, 30m)
            },
            "token");

        _inventory.Setup(x => x.ReserveAsync(
            "P1", 1, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _inventory.Setup(x => x.ReserveAsync(
            "P2", 1, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var sut = CreateSut();

        Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.PlaceOrderAsync(request));

        _inventory.Verify(x => x.ReleaseAsync(
            "P1", 1, It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        _payment.Verify(x => x.ChargeAsync(
            It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestCase("")]
    [TestCase("   ")]
    public void PlaceOrderAsync_WhenCustomerIdIsInvalid_DoesNotCallDependencies(string customerId)
    {
        var request = new PlaceOrderRequest(
            customerId, Array.Empty<OrderItem>(), "token");

        var sut = CreateSut();

        Assert.ThrowsAsync<ArgumentException>(async () => await sut.PlaceOrderAsync(request));

        _inventory.Verify(x => x.ReserveAsync(
            It.IsAny<string>(), It.IsAny<int>(),
            It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        _payment.Verify(x => x.ChargeAsync(
            It.IsAny<string>(), It.IsAny<decimal>(),
            It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}

