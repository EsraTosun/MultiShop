using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer;
using MultiShop.IdentityServer.Data;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// -----------------------------
// Serilog
// -----------------------------
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

// -----------------------------
// DbContext
// -----------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -----------------------------
// Identity
// -----------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true; 
    //options.SignIn.RequireConfirmedEmail = true; 
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// -----------------------------
// IdentityServer
// -----------------------------
builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
})
.AddAspNetIdentity<ApplicationUser>()
.AddInMemoryIdentityResources(Config.IdentityResources)
.AddInMemoryApiScopes(Config.ApiScopes)
.AddInMemoryApiResources(Config.ApiResources) 
.AddInMemoryClients(Config.Clients)
.AddDeveloperSigningCredential(); // Dev only



// -----------------------------
// ProfileService (claim eklemek için)
// -----------------------------
builder.Services.AddScoped<IProfileService, ProfileService>();

// -----------------------------
// Local API Authentication (Bearer token)
// -----------------------------
builder.Services.AddAuthentication()
    .AddLocalApi("Bearer", options =>
    {
        options.ExpectedScope = IdentityServerConstants.LocalApi.ScopeName;
    });

// -----------------------------
// Controllers
// -----------------------------
builder.Services.AddControllers();

var app = builder.Build();
app.MapDefaultEndpoints();

// -----------------------------
// Middleware
// -----------------------------
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// -----------------------------
// Seed Data (opsiyonel)
// -----------------------------
if (args.Contains("/seed"))
{
    Log.Information("Seeding database...");
    SeedData.EnsureSeedData(app);
    Log.Information("Done seeding database. Exiting.");
    return;
}

app.Run();
