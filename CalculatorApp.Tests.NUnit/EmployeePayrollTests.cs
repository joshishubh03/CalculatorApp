using EmployeePayroll;
using NUnit.Framework;

namespace CalculatorApp.Tests.NUnit;

[TestFixture]
public class EmployeePayrollTests
{
    private EmployeePayrollService _service = null!;

    [SetUp]
    public void Setup()
    {
        _service = new EmployeePayrollService();
    }

    [Test]
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
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CalculateBonus_EligibleEmployee_ReturnsTenPercent()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Amit",
            MonthlySalary = 50000,
            ExperienceYears = 3
        };

        // Act
        decimal actual = _service.CalculateBonus(employee);

        // Assert
        Assert.That(actual, Is.EqualTo(5000m));
    }

    [Test]
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
        Assert.That(actual, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateAnnualCompensation_EligibleEmployee_ReturnsCorrectAmount()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Rohit",
            MonthlySalary = 50000,
            ExperienceYears = 5
        };

        // Act
        decimal actual =
            _service.CalculateAnnualCompensation(employee);

        // Assert
        Assert.That(actual, Is.EqualTo(660000m));
    }

    [Test]
    public void CalculateAnnualSalary_NullEmployee_ThrowsException()
    {
        // Arrange
        Employee? employee = null;

        // Act and Assert
        Assert.Throws<ArgumentNullException>(
            () => _service.CalculateAnnualSalary(employee!));
    }
}
