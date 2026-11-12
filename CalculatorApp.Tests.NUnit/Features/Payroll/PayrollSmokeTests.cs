using EmployeePayroll;
using NUnit.Framework;

[TestFixture]
public class PayrollSmokeTests
{
    [Test]
    [Category("Smoke")]
    public void AnnualSalary_ForValidEmployee_ReturnsTwelveMonths()
    {
        var employee = new Employee
        {
            Name = "Asha",
            MonthlySalary = 50000m,
            ExperienceYears = 3
        };

        var service = new EmployeePayrollService();

        Assert.That(service.CalculateAnnualSalary(employee), Is.EqualTo(600000m));
    }

    [Test]
    [Category("Regression")]
    public void Bonus_ForEmployeeBelowEligibility_ReturnsZero()
    {
        var employee = new Employee
        {
            Name = "Ravi",
            MonthlySalary = 40000m,
            ExperienceYears = 1
        };

        var service = new EmployeePayrollService();

        Assert.That(service.CalculateBonus(employee), Is.EqualTo(0m));
    }
}
