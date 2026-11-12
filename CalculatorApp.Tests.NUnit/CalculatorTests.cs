using CalculatorApp;
using NUnit.Framework;

namespace CalculatorApp.Tests.NUnit;

[TestFixture]
public class CalculatorTests
{
    [Test]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange
        var calculator = new Calculator();
        var result = calculator.Add(10, 5);
        Assert.That(result, Is.EqualTo(15));
    }

    [Test]
    public void Subtract_TwoNumbers_ReturnsDifference()
    {
        var calculator = new Calculator();
        var result = calculator.Subtract(10, 5);
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Multiply_TwoNumbers_ReturnsProduct()
    {
        var calculator = new Calculator();
        var result = calculator.Multiply(10, 5);
        Assert.That(result, Is.EqualTo(50));
    }

    [Test]
    public void Divide_TwoNumbers_ReturnsQuotient()
    {
        var calculator = new Calculator();
        var result = calculator.Divide(10, 5);
        Assert.That(result, Is.EqualTo(2.0));
    }

    [Test]
    public void Divide_ByZero_ThrowsException()
    {
        var calculator = new Calculator();

        Assert.Throws<DivideByZeroException>(
            () => calculator.Divide(10, 0));
    }
}
