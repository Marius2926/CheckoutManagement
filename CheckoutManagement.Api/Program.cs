using CheckoutManagement.Api;
using CheckoutManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterCheckoutModule(builder.Configuration);

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var hostEnvironment = services.GetService<IWebHostEnvironment>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogInformation(message: $"Starting in environment {hostEnvironment.EnvironmentName}");
    try
    {
        var seedService = services.GetRequiredService<AppDbContextSeed>();
        await seedService.SeedAsync();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.MapCheckoutEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();