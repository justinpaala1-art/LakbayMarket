using System.Net;
using System.Net.Http.Json;
using LakbayMarket.Contracts.Health;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace LakbayMarket.Tests.Integration;

public sealed class HealthEndpointTests : IClassFixture<ApiFactory>
{
    private const string ProblemJson = "application/problem+json";

    private readonly HttpClient _client;

    public HealthEndpointTests(ApiFactory factory)
    {
        // An https base address avoids the HTTPS-redirection middleware answering with a redirect.
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
        });
    }

    [Fact]
    public async Task Health_returns_200_with_a_healthy_utc_response()
    {
        var response = await _client.GetAsync("/api/v1/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<HealthResponse>();
        Assert.NotNull(body);
        Assert.Equal("Healthy", body.Status);
        Assert.Equal(DateTimeKind.Utc, body.TimestampUtc.Kind);
    }

    [Fact]
    public async Task Unknown_route_returns_404_as_problem_details()
    {
        var response = await _client.GetAsync("/api/v1/does-not-exist");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal(ProblemJson, response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task Ready_returns_503_as_problem_details_when_the_database_is_unreachable()
    {
        var response = await _client.GetAsync("/api/v1/health/ready");

        Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        Assert.Equal(ProblemJson, response.Content.Headers.ContentType?.MediaType);
    }
}
