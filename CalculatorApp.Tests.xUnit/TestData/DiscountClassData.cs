using Xunit;

namespace CalculatorApp.Tests.xUnit.TestData;

public class DiscountClassData :
    TheoryData<decimal, string, string?, decimal>
{
    public DiscountClassData()
    {
        Add(6000, "Gold", "SAVE10", 25);
        Add(3000, "Silver", "SAVE10", 17);
        Add(10000, "Platinum", "SAVE20", 30);
        Add(1000, "Regular", "INVALID", 5);
        Add(500, "Regular", null, 0);
    }
}
