using EmployeesApi;
using EmployeesApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("employees") ?? throw new Exception("The connection string is missing");

// One Scoped Service called EmployeesDataContext, and one Singleton Service that manages the connections to the database.
builder.Services.AddDbContext<EmployeesDataContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// When it goes to create our EmployeesController, it needs something that can manage employees.
builder.Services.AddScoped<IManageEmployees, EfEmployeeManager>();
builder.Services.AddScoped<IManageCandidates, EfCandidatesManager>();


// 120ms

builder.Services.AddEmployeeMapperProfiles();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
