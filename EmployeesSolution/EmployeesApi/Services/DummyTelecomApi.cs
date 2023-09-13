namespace EmployeesApi.Services;

public class DummyTelecomApi : IProvideTheTelecomApi
{
    public async Task<EmployeeContactMechanismsResponse> GetPhoneAndEmailAssignmentAsync(string firstName, string lastName)
    {
        return new EmployeeContactMechanismsResponse
        {
            Email = $"{firstName.ToLower()}_{lastName.ToLower()}@company.com",
            PhoneNumber = "555-1212xt42069"
        };
    }
}
