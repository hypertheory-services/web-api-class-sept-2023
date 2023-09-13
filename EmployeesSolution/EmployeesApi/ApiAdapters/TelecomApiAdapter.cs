using static EmployeesApi.ApiAdapters.TelecomRequestModel;

namespace EmployeesApi.ApiAdapters;

public class TelecomApiAdapter : IProvideTheTelecomApi
{
    private readonly HttpClient _httpClient;

    public TelecomApiAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EmployeeContactMechanismsResponse> GetPhoneAndEmailAssignmentAsync(string firstName, string lastName)
    {
        var response = await _httpClient.PostAsJsonAsync("/contact-information-assignments", TelecomRequestModel.From(firstName, lastName));

        response.EnsureSuccessStatusCode(); 

        var apiResponse = await response.Content.ReadFromJsonAsync<TelecomResponseModel>();
        if(apiResponse is null)
        {
            throw new InvalidOperationException("Api Response is Null");
        }
        return new EmployeeContactMechanismsResponse()
        {
            Email = apiResponse.Email,
            PhoneNumber = $"{apiResponse.PhoneNumber}{apiResponse.PhoneExtension}"
        };
    }
}

public record TelecomRequestModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public static TelecomRequestModel From(string firstName, string lastName)
    {
        return new TelecomRequestModel { FirstName = firstName, LastName = lastName };
    }
}


public record TelecomResponseModel
{
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PhoneExtension { get; set; } = string.Empty;

}
