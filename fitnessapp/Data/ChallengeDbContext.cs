using fitnessapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fitnessapp.Data;

public class ChallengeDbContext : IdentityDbContext
{
    public ChallengeDbContext(){}
    public ChallengeDbContext(DbContextOptions<ChallengeDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString ("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<TblChallangelist> Challenges { get; set; }
}
