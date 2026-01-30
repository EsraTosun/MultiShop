using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ================= AUTH =================
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("OcelotAuthenticationScheme", options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"];
        options.RequireHttpsMetadata = false;

        // IdentityServer'daki LOCAL API
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false
        };
    });

// ================= OCELOT =================
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// 🔴 ÇOK ÖNEMLİ
app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.MapGet("/", () => "Ocelot Gateway is running 🚀");

app.Run();
