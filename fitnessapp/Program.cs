using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using fitnessapp.Data;
using fitnessapp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("DefaultConnection"));
var connectionString2 = builder.Configuration.GetConnectionString("MyConnection") ?? throw new InvalidOperationException("Connection string 'MyConnection' not found.");
builder.Services.AddDbContext<UserChallengeDatabaseContext>(options =>options.UseSqlServer(connectionString2));
var connectionString3 = builder.Configuration.GetConnectionString("SecondConnection") ?? throw new InvalidOperationException("Connection string 'SecondConnection' not found.");
builder.Services.AddDbContext<ChallengeDatabaseContext>(options =>options.UseSqlServer(connectionString3));

//Seasion handler
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".UserInformation.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
