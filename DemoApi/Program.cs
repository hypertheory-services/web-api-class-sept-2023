var builder = WebApplication.CreateBuilder(args);

// Configuration for the "internals" for the application, which is mostly services and overriding the way stuff works.
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// everything after here is "Middleware" - which is handing incoming requests and outgoing resposes.
// We will create some middleware later.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// this looks at all of our controllers, reads the routing attributes, and creates a "lookup table" so that
// when requests come in, they can be sent to the right code to process that request.
app.MapControllers();
// IF I get GET for /blog/int/int/int -> InfoController -> GetBlog

//app.MapGet("/info", () =>
//{
//    return "All Good Here";
//});

app.Run(); // Blocking Call
