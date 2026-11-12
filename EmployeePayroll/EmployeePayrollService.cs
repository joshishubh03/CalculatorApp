namespace EmployeePayroll;

public class EmployeePayrollService
{
    public decimal CalculateAnnualSalary(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        return employee.MonthlySalary * 12;
    }

    public decimal CalculateBonus(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (employee.ExperienceYears < 2)
        {
            return 0;
        }

        return employee.MonthlySalary * 0.10m;
    }

    public bool IsEligibleForSalaryIncrement(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        return employee.ExperienceYears >= 3
            && employee.MonthlySalary >= 30000m;
    }

    public decimal CalculateSalaryIncrement(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (employee.ExperienceYears >= 5)
        {
            return employee.MonthlySalary * 0.15m;
        }

        if (employee.ExperienceYears >= 3)
        {
            return employee.MonthlySalary * 0.10m;
        }

        return 0m;
    }

    public decimal CalculateAnnualCompensation(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        return CalculateAnnualSalary(employee)
                + (CalculateBonus(employee) * 12);
    }
}
