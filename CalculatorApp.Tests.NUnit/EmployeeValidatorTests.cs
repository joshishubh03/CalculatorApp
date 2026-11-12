using EmployeePayroll;
using NUnit.Framework;

namespace CalculatorApp.Tests.NUnit;

[TestFixture]
public class EmployeeValidatorTests
{
    private EmployeeValidator _validator = null!;

    [SetUp]
    public void Setup()
    {
        _validator = new EmployeeValidator();
    }

    [Test]
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
        Assert.That(result, Is.True);
    }

    [Test]
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
        Assert.That(
            exception!.Message,
            Is.EqualTo("Employee name is required."));
    }

    [Test]
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

    [Test]
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
