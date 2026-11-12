using EmployeePayroll;
using Xunit;

namespace CalculatorApp.Tests.xUnit.Features.Payroll;

public class EmployeeValidatorTests
{
    private readonly EmployeeValidator _validator;

    public EmployeeValidatorTests()
    {
        _validator = new EmployeeValidator();
    }

    [Fact]
    public void IsValid_ValidEmployee_ReturnsTrue()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Rahul",
            MonthlySalary = 50000,
            ExperienceYears = 5
        };

        // Act
        bool result = _validator.IsValid(employee);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_EmptyName_ThrowsArgumentException()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "",
            MonthlySalary = 50000,
            ExperienceYears = 5
        };

        // Act
        var exception = Assert.Throws<ArgumentException>(
            () => _validator.IsValid(employee));

        // Assert
        Assert.Equal(
            "Employee name is required.",
            exception.Message);
    }

    [Fact]
    public void IsValid_ZeroSalary_ThrowsArgumentException()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Rahul",
            MonthlySalary = 0,
            ExperienceYears = 5
        };

        // Act and Assert
        Assert.Throws<ArgumentException>(
            () => _validator.IsValid(employee));
    }

    [Fact]
    public void IsValid_NegativeExperience_ThrowsArgumentException()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Rahul",
            MonthlySalary = 50000,
            ExperienceYears = -1
        };

        // Act and Assert
        Assert.Throws<ArgumentException>(
            () => _validator.IsValid(employee));
    }
}
