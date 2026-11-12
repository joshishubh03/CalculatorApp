using CalculatorApp;
using Xunit;

namespace CalculatorApp.Tests.xUnit;

public class CalculatorTests
{
	[Fact]
	public void Add_TwoPositiveNumbers_ReturnsSum()
	{
		// Arrange
		var calculator = new Calculator();

		// Act
		var result = calculator.Add(10, 5);

		// Assert
		Assert.Equal(15, result);
	}

	[Fact]
	public void Subtract_TwoNumbers_ReturnsDifference()
	{
		// Arrange
		var calculator = new Calculator();
		var result = calculator.Subtract(10, 5);
		Assert.Equal(5, result);
	}

	[Fact]
	public void Multiply_TwoNumbers_ReturnsProduct()
	{
		// Arrange
		var calculator = new Calculator();
		var result = calculator.Multiply(10, 5);
		Assert.Equal(50, result);
	}

	[Fact]
	public void Divide_TwoNumbers_ReturnsQuotient()
	{
		// Arrange
		var calculator = new Calculator();
		var result = calculator.Divide(10, 5);
		Assert.Equal(2.0, result);
	}

	[Fact]
	public void Divide_ByZero_ThrowsException()
	{
		// Arrange
		var calculator = new Calculator();

		// Act and Assert
		Assert.Throws<DivideByZeroException>(
			() => calculator.Divide(10, 0));
	}
}
