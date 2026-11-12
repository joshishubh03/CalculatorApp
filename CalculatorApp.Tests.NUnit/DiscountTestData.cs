namespace CalculatorApp.Tests.NUnit;

public static class DiscountTestData
{
    public static IEnumerable<object[]> OrderDiscountCases()
    {
        yield return new object[] { 500m, 0m };
        yield return new object[] { 1000m, 5m };
        yield return new object[] { 3000m, 5m };
        yield return new object[] { 5000m, 10m };
        yield return new object[] { 9000m, 10m };
        yield return new object[] { 10000m, 15m };
        yield return new object[] { 15000m, 15m };
    }

    public static IEnumerable<object[]> MembershipCases()
    {
        yield return new object[] { "Regular", 0m };
        yield return new object[] { "Silver", 2m };
        yield return new object[] { "Gold", 5m };
        yield return new object[] { "Platinum", 10m };
    }
}
