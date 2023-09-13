namespace TelecomApi.Services;

public class FakeEmailAndPhoneNumbers : ICreateEmailAndPhoneNumbers
{
    
    public async Task<ContactAssignmentResponse> AssignContactInfoAsync(ContactAssignmentRequest request)
    {
        return new ContactAssignmentResponse
        {
            Email = $"{request.FirstName.ToLower()}_{request.LastName.ToLower()}@company.com",
            PhoneNumber = "555-1212",
            PhoneExtension = "ext" + new Random().Next(100, 999)
        };
    }
}
