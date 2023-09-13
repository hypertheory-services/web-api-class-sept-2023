using Microsoft.AspNetCore.Mvc;
using TelecomApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICreateEmailAndPhoneNumbers, FakeEmailAndPhoneNumbers>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/contact-information-assignments", async ([FromBody] ContactAssignmentRequest request, [FromServices] ICreateEmailAndPhoneNumbers service) =>
{
    ContactAssignmentResponse response = await service.AssignContactInfoAsync(request);
    return Results.Ok(response);
});


app.Run();

// First Name and Last Name
// (EmailAddress, PhoneNumber)

public record ContactAssignmentRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public record ContactAssignmentResponse
{
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PhoneExtension { get; set; } = string.Empty;
}