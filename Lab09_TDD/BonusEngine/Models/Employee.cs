namespace EmployeeOnboarding.Core.Models;


public sealed record Employee(
    int Id,
    string Name,
    string Email,
    string Department);


public interface IEmployeeRepository
{
    Task<int> AddAsync(EmployeeInput input, CancellationToken cancellationToken = default);
}

public interface IAccountProvisioner
{
    Task ProvisionAsync(string email, CancellationToken cancellationToken = default);
}

public interface IWelcomeNotifier
{
    Task SendAsync(string email, string employeeName, CancellationToken cancellationToken = default);
}
