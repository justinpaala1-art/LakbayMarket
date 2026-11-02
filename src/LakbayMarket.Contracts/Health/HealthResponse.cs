namespace LakbayMarket.Contracts.Health;

/// <summary>Response body for <c>GET /api/v1/health</c>.</summary>
/// <param name="Status">"Healthy" when the API is able to serve requests.</param>
/// <param name="TimestampUtc">Server time in UTC when the response was produced.</param>
public sealed record HealthResponse(string Status, DateTime TimestampUtc);
