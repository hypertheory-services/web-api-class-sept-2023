namespace EmployeesApi.Services;

public interface IProvideTheTelecomApi
{
    Task<EmployeeContactMechanismsResponse> GetPhoneAndEmailAssignmentAsync(string firstName, string lastName);
}
