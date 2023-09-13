namespace TelecomApi.Services;

public interface ICreateEmailAndPhoneNumbers
{
    Task<ContactAssignmentResponse> AssignContactInfoAsync(ContactAssignmentRequest request);
}
