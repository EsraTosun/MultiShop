using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;
using MultiShop.Message.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// ?? JWT Authentication
builder.Services.AddAuthentication("OcelotAuthenticationScheme")
    .AddJwtBearer("OcelotAuthenticationScheme", options =>
    {
        options.Authority = "http://identity";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidAudiences = new[] { "message.api" },
            ValidateIssuer = true,
            ValidateLifetime = true,
            NameClaimType = "name",
            RoleClaimType = "role"
        };

        options.MapInboundClaims = false; 
    });


// ??? Database (PostgreSQL)
builder.Services.AddDbContext<MessageContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ?? DI
builder.Services.AddScoped<IUserMessageService, UserMessageService>();

// ?? AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// ?? Controllers
builder.Services.AddControllers();

// ?? Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapDefaultEndpoints();

// ?? Middleware
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
