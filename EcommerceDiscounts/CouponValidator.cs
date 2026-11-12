namespace EcommerceDiscounts;

public class CouponValidator
{
    public decimal GetCouponDiscount(string? couponCode)
    {
        return couponCode?.Trim().ToUpperInvariant() switch
        {
            "SAVE10" => 10m,
            "SAVE20" => 20m,
            _ => 0m
        };
    }
}
