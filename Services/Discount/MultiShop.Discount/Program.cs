using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Discount.Context;
using MultiShop.Discount.Extensions;
using MultiShop.Discount.Filters;
using MultiShop.Discount.Services;
using MultiShop.Discount.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// ===== JWT Authentication =====
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.Authority = builder.Configuration["IdentityServerUrl"];
        opt.RequireHttpsMetadata = false;

        opt.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidAudiences = new[] { "discount.api" },
            ValidateIssuer = true,
            ValidateLifetime = true,
            NameClaimType = "name",
            RoleClaimType = "role"
        };

        opt.MapInboundClaims = false; 
    });


// ===== Dapper Context =====
builder.Services.AddSingleton<DapperContext>();

// ===== DB Initializer =====
builder.Services.AddSingleton<DbInitializer>();

// ===== Services =====
builder.Services.AddTransient<IDiscountService, DiscountService>();

// ===== Controllers =====
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.AddDiscountValidators();

// ===== Swagger =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<MultiShop.Discount.Filters.SwaggerResponseOperationFilter>();
});

var app = builder.Build();
app.MapDefaultEndpoints();

// ===== DB AUTO CREATE =====
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    initializer.Initialize();
}

// ===== Middleware =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
