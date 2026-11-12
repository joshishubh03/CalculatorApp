using EmployeePayroll;
using Xunit;

namespace CalculatorApp.Tests.xUnit.Features.Payroll;

public class EmployeePayrollTests
{
    private readonly EmployeePayrollService _service;

    public EmployeePayrollTests()
    {
        _service = new EmployeePayrollService();
    }

    [Fact]
    public void CalculateAnnualSalary_ValidEmployee_ReturnsCorrectSalary()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Rahul",
            MonthlySalary = 50000,
            ExperienceYears = 5
        };
        decimal expected = 600000;

        // Act
        decimal actual =
            _service.CalculateAnnualSalary(employee);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateBonus_EligibleEmployee_ReturnsTenPercent()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Amit",
            MonthlySalary = 50000,
            ExperienceYears = 3
        };
        decimal expected = 5000;

        // Act
        decimal actual = _service.CalculateBonus(employee);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateBonus_IneligibleEmployee_ReturnsZero()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Neha",
            MonthlySalary = 50000,
            ExperienceYears = 1
        };

        // Act
        decimal actual = _service.CalculateBonus(employee);

        // Assert
        Assert.Equal(0m, actual);
    }

    [Fact]
    public void CalculateAnnualCompensation_EligibleEmployee_ReturnsCorrectAmount()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Rohit",
            MonthlySalary = 50000,
            ExperienceYears = 5
        };
        decimal expected = 660000;

        // Act
        decimal actual =
            _service.CalculateAnnualCompensation(employee);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateAnnualSalary_NullEmployee_ThrowsException()
    {
        // Arrange
        Employee? employee = null;

        // Act and Assert
        Assert.Throws<ArgumentNullException>(
            () => _service.CalculateAnnualSalary(employee!));
    }
}
