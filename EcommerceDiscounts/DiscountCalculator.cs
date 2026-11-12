namespace EcommerceDiscounts;

public class DiscountCalculator
{
    private readonly CouponValidator _couponValidator = new();

    public decimal GetOrderDiscount(decimal orderAmount)
    {
        if (orderAmount < 0)
            throw new ArgumentException("Order amount cannot be negative.");

        return orderAmount switch
        {
            < 1000 => 0m,
            < 5000 => 5m,
            < 10000 => 10m,
            _ => 15m
        };
    }

    public decimal GetMembershipDiscount(string membership)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(membership);

        return membership.Trim().ToUpperInvariant() switch
        {
            "REGULAR" => 0m,
            "SILVER" => 2m,
            "GOLD" => 5m,
            "PLATINUM" => 10m,
            _ => throw new ArgumentException("Invalid membership type.")
        };
    }

    public decimal CalculateTotalDiscount(
        decimal orderAmount, string membership, string? couponCode)
    {
        if (orderAmount <= 0)
            throw new ArgumentException("Order amount must be greater than zero.");

        decimal total = GetOrderDiscount(orderAmount)
            + GetMembershipDiscount(membership)
            + _couponValidator.GetCouponDiscount(couponCode);

        return Math.Min(total, 30m);
    }

    public decimal CalculateFinalAmount(
        decimal orderAmount, string membership, string? couponCode)
    {
        decimal discount = CalculateTotalDiscount(
            orderAmount, membership, couponCode);

        return orderAmount - (orderAmount * discount / 100m);
    }
}
