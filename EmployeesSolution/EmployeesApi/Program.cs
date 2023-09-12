using EmployeesApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("employees") ?? throw new Exception("The connection string is missing");

builder.Services.AddDbContext<EmployeesDataContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// When it goes to create our EmployeesController, it needs something that can manage employees.
builder.Services.AddScoped<IManageEmployees, EfEmployeeManager>();

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
