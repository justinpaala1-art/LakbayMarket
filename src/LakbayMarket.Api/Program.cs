using LakbayMarket.Contracts.Health;
using LakbayMarket.Infrastructure;
using LakbayMarket.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// The connection string is a secret: dotnet user-secrets locally, environment variables or a managed
// secret store when hosted. Never put it in appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException(
        "Connection string 'Default' is not configured. Set ConnectionStrings:Default with dotnet user-secrets.");

builder.Services.AddInfrastructure(connectionString);

// Standard error contract (Problem Details) for unhandled exceptions and bare status codes such as 404.
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseHttpsRedirection();

// Liveness: the process is up and can answer requests. No dependencies are checked.
app.MapGet("/api/v1/health", () => TypedResults.Ok(new HealthResponse("Healthy", DateTime.UtcNow)));

// Readiness: the API can also reach its database.
app.MapGet("/api/v1/health/ready", async (LakbayDbContext db, CancellationToken cancellationToken) =>
{
    var canConnect = await db.Database.CanConnectAsync(cancellationToken);

    return canConnect
        ? Results.Ok(new HealthResponse("Ready", DateTime.UtcNow))
        : Results.Problem(title: "Database unavailable", statusCode: StatusCodes.Status503ServiceUnavailable);
});

app.Run();

// Lets the integration-test project reference the entry point later.
public partial class Program { }
