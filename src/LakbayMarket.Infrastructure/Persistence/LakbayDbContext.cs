using Microsoft.EntityFrameworkCore;

namespace LakbayMarket.Infrastructure.Persistence;

/// <summary>
/// EF Core database context for Lakbay Market.
/// The model is intentionally empty for now: entities are added feature by feature
/// (Identity first, in Week 2), each with its own migration.
/// </summary>
public sealed class LakbayDbContext(DbContextOptions<LakbayDbContext> options) : DbContext(options)
{
}
