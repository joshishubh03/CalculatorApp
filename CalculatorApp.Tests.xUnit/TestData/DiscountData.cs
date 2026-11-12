using Xunit;

namespace CalculatorApp.Tests.xUnit.TestData;

public static class DiscountData
{
    public static TheoryData<decimal, decimal> OrderDiscountCases =>
        new()
        {
            { 500, 0 }, { 1000, 5 }, { 3000, 5 },
            { 5000, 10 }, { 9000, 10 }, { 10000, 15 },
            { 15000, 15 }
        };

    public static TheoryData<string, decimal> MembershipCases =>
        new()
        {
            { "Regular", 0 }, { "Silver", 2 },
            { "Gold", 5 }, { "Platinum", 10 }
        };
}
