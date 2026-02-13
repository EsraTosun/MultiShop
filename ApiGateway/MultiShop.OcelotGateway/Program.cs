using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// ----------------------
// Configuration
// ----------------------
builder.Configuration
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
       .AddEnvironmentVariables();

// ----------------------
// Authentication & Authorization
// ----------------------
builder.Services.AddAuthentication()
    .AddJwtBearer("OcelotAuthenticationScheme", options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"];
        options.Audience = "ocelot.api";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

builder.Services.AddAuthorization();

// ----------------------
// Ocelot
// ----------------------
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.MapDefaultEndpoints();

// ----------------------
// Middleware
// ----------------------
app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

// ----------------------
// Test endpoint
// ----------------------
app.MapGet("/", () => "Ocelot Gateway is running");

app.Run();
