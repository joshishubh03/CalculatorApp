using EmployeePayroll;
using Xunit;

public class PayrollSmokeTests
{
    [Fact]
    [Trait("Category", "Smoke")]
    public void AnnualSalary_ForValidEmployee_ReturnsTwelveMonths()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Asha",
            MonthlySalary = 50000m,
            ExperienceYears = 3
        };

        var service = new EmployeePayrollService();

        // Act
        var result = service.CalculateAnnualSalary(employee);

        // Assert
        Assert.Equal(600000m, result);
    }

    [Fact]
    [Trait("Category", "Regression")]
    public void Bonus_ForEmployeeBelowEligibility_ReturnsZero()
    {
        var employee = new Employee
        {
            Name = "Ravi",
            MonthlySalary = 40000m,
            ExperienceYears = 1
        };

        var service = new EmployeePayrollService();

        Assert.Equal(0m, service.CalculateBonus(employee));
    }
}
