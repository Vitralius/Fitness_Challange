using fitnessapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;

namespace fitnessapp.Data;

public class UserChallangeDbContext : DbContext
{
    public UserChallangeDbContext(){}
    public UserChallangeDbContext(DbContextOptions<UserChallangeDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString ("MyConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<TblChallange> tbl_challange { get; set; }
}
