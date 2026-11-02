using LakbayMarket.Contracts.Health;

var builder = WebApplication.CreateBuilder(args);

// Standard error contract (Problem Details) for unhandled exceptions and bare status codes such as 404.
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseHttpsRedirection();

// Liveness probe. A database check is added once EF Core is wired up.
app.MapGet("/api/v1/health", () => TypedResults.Ok(new HealthResponse("Healthy", DateTime.UtcNow)));

app.Run();

// Lets the integration-test project reference the entry point later.
public partial class Program { }
