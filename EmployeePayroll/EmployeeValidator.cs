namespace EmployeePayroll;

public class EmployeeValidator
{
    public bool IsValid(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (string.IsNullOrWhiteSpace(employee.Name))
        {
            throw new ArgumentException(
                "Employee name is required.");
        }

        if (employee.MonthlySalary <= 0)
        {
            throw new ArgumentException(
                "Monthly salary must be greater than zero.");
        }

        if (employee.ExperienceYears < 0)
        {
            throw new ArgumentException(
                "Experience cannot be negative.");
        }

        return true;
    }
}
