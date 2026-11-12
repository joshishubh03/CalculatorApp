using EcommerceDiscounts;
using NUnit.Framework;

namespace CalculatorApp.Tests.NUnit;

[TestFixture]
public class DiscountCalculatorTests
{
    private DiscountCalculator _calculator = null!;

    [SetUp]
    public void Setup() => _calculator = new DiscountCalculator();

    [TestCase(500, 0)]
    [TestCase(1000, 5)]
    [TestCase(3000, 5)]
    [TestCase(5000, 10)]
    [TestCase(9000, 10)]
    [TestCase(10000, 15)]
    [TestCase(15000, 15)]
    public void GetOrderDiscount_VariousAmounts_ReturnsExpectedDiscount(
        decimal amount, decimal expected)
    {
        decimal actual = _calculator.GetOrderDiscount(amount);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("Regular", 0)]
    [TestCase("Silver", 2)]
    [TestCase("Gold", 5)]
    [TestCase("Platinum", 10)]
    public void GetMembershipDiscount_ValidMembership_ReturnsExpectedDiscount(
        string membership, decimal expected)
    {
        decimal actual = _calculator.GetMembershipDiscount(membership);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("SAVE10", 10)]
    [TestCase("SAVE20", 20)]
    [TestCase("INVALID", 0)]
    [TestCase(null, 0)]
    public void GetCouponDiscount_VariousCoupons_ReturnsExpectedDiscount(
        string? coupon, decimal expected)
    {
        var validator = new CouponValidator();
        decimal actual = validator.GetCouponDiscount(coupon);
        Assert.That(actual, Is.EqualTo(expected));
    }
    [TestCaseSource(typeof(DiscountTestData),
    nameof(DiscountTestData.OrderDiscountCases))]
    public void GetOrderDiscount_TestCaseSource_ReturnsExpectedDiscount(
    decimal amount, decimal expected)
    {
        decimal actual = _calculator.GetOrderDiscount(amount);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCaseSource(typeof(DiscountTestData),
        nameof(DiscountTestData.MembershipCases))]
    public void GetMembershipDiscount_TestCaseSource_ReturnsExpectedDiscount(
        string membership, decimal expected)
    {
        decimal actual = _calculator.GetMembershipDiscount(membership);
        Assert.That(actual, Is.EqualTo(expected));
    }

}
