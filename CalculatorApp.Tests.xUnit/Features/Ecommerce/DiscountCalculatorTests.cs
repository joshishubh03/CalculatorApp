using CalculatorApp.Tests.xUnit.TestData;
using EcommerceDiscounts;
using Xunit;

namespace CalculatorApp.Tests.xUnit.Features.Ecommerce;

public class DiscountCalculatorTests
{
    private readonly DiscountCalculator _calculator = new();

    [Theory]
    [InlineData(500, 0)]
    [InlineData(1000, 5)]
    [InlineData(3000, 5)]
    [InlineData(5000, 10)]
    [InlineData(9000, 10)]
    [InlineData(10000, 15)]
    [InlineData(15000, 15)]
    public void GetOrderDiscount_VariousAmounts_ReturnsExpectedDiscount(
        decimal amount, decimal expected)
    {
        // Arrange

        // Act
        decimal actual = _calculator.GetOrderDiscount(amount);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Regular", 0)]
    [InlineData("Silver", 2)]
    [InlineData("Gold", 5)]
    [InlineData("Platinum", 10)]
    public void GetMembershipDiscount_ValidMembership_ReturnsExpectedDiscount(
        string membership, decimal expected)
    {
        decimal actual = _calculator.GetMembershipDiscount(membership);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("SAVE10", 10)]
    [InlineData("SAVE20", 20)]
    [InlineData("INVALID", 0)]
    [InlineData(null, 0)]
    public void GetCouponDiscount_VariousCoupons_ReturnsExpectedDiscount(
        string? coupon, decimal expected)
    {
        var validator = new CouponValidator();
        decimal actual = validator.GetCouponDiscount(coupon);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(DiscountData.OrderDiscountCases),
    MemberType = typeof(DiscountData))]
    public void GetOrderDiscount_MemberData_ReturnsExpectedDiscount(
    decimal amount, decimal expected)
    {
        decimal actual = _calculator.GetOrderDiscount(amount);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(DiscountData.MembershipCases),
        MemberType = typeof(DiscountData))]
    public void GetMembershipDiscount_MemberData_ReturnsExpectedDiscount(
        string membership, decimal expected)
    {
        decimal actual = _calculator.GetMembershipDiscount(membership);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(DiscountClassData))]
    public void CalculateTotalDiscount_ClassData_ReturnsExpectedDiscount(
    decimal amount, string membership, string? coupon, decimal expected)
    {
        decimal actual = _calculator.CalculateTotalDiscount(
            amount, membership, coupon);

        Assert.Equal(expected, actual);
    }

}
