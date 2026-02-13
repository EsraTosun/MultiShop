using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;
using MultiShop.Basket.Settings;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// 🧠 Claim mapping kapatılır
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// 🔐 Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.Authority = "http://identity";
        opt.RequireHttpsMetadata = false;

        opt.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidAudiences = new[] { "basket.api" },
            ValidateLifetime = true,
            NameClaimType = "name",
            RoleClaimType = "role"
        };

        opt.MapInboundClaims = false;
    });

// 🔐 Authorization (fallback policy)
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddHttpContextAccessor();

// 🧩 Application services
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IBasketService, BasketService>();

// ⚙ Redis configuration
builder.Services.Configure<RedisSettings>(
    builder.Configuration.GetSection(nameof(RedisSettings))
);

// ⚠ Redis connection – fail safe
builder.Services.AddSingleton<RedisService>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    var redis = new RedisService(settings.Host, settings.Port);

    try
    {
        redis.Connect();
        Console.WriteLine("[Redis] Connected");
    }
    catch (Exception ex)
    {
        Console.WriteLine("[Redis] Connection failed: " + ex.Message);
    }

    return redis;
});

// 🎮 Controllers
builder.Services.AddControllers();

// 📘 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
