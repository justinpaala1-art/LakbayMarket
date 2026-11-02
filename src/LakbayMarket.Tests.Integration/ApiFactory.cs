using Microsoft.AspNetCore.Mvc.Testing;

namespace LakbayMarket.Tests.Integration;

/// <summary>
/// Starts the API in memory for tests.
/// The connection string points at a port nothing listens on, so no test can reach a real database,
/// including the developer's local one.
/// </summary>
public sealed class ApiFactory : WebApplicationFactory<global::Program>
{
    private const string UnreachableConnectionString =
        "Server=127.0.0.1;Port=1;Database=lakbay_test;User=test;Password=test;Connection Timeout=2";

    public ApiFactory()
    {
        // Program.cs reads the connection string while it builds the host, before the factory's own
        // configuration callbacks run, so the value is supplied as an environment variable. Environment
        // variables also take precedence over the developer's user-secrets.
        Environment.SetEnvironmentVariable("ConnectionStrings__Default", UnreachableConnectionString);
    }
}
