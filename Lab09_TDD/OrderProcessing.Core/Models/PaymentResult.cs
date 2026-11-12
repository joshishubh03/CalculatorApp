namespace OrderProcessing.Core.Models
{
    public sealed record PaymentResult(
        bool Succeeded,
        string? TransactionId,
        string? FailureReason);
}

namespace OrderProcessing.Core.Abstractions
{
    using OrderProcessing.Core.Models;

    public interface IInventoryService
    {
        Task<bool> ReserveAsync(
            string productId,
            int quantity,
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task ReleaseAsync(
            string productId,
            int quantity,
            Guid orderId,
            CancellationToken cancellationToken = default);
    }

    public interface IPaymentService
    {
        Task<PaymentResult> ChargeAsync(
            string paymentMethodToken,
            decimal amount,
            Guid orderId,
            CancellationToken cancellationToken = default);
    }

    public interface IOrderRepository
    {
        Task SaveConfirmedAsync(
            Guid orderId,
            string customerId,
            IReadOnlyList<OrderItem> items,
            decimal total,
            string transactionId,
            CancellationToken cancellationToken = default);
    }

    public interface IOrderNotificationService
    {
        Task SendConfirmationAsync(
            Guid orderId,
            string customerId,
            decimal total,
            CancellationToken cancellationToken = default);
    }
}
