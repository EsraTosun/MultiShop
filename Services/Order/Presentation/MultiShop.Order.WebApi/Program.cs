using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Order.Application.Features.Addresses.Commands.Create;
using MultiShop.Order.Application.Features.Addresses.Queries.GetAll;
using MultiShop.Order.Application.Features.OrderDetails.Commands.Create;
using MultiShop.Order.Application.Features.OrderDetails.Queries.GetAll;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Application.Mapping;
using MultiShop.Order.Application.Services;
using MultiShop.Order.Persistence.Context;
using MultiShop.Order.Persistence.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ===== Authentication =====
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.Authority = builder.Configuration["IdentityServerUrl"];
        opt.Audience = "ResourceOrder";
        opt.RequireHttpsMetadata = false;
    });

// ===== DbContext =====
builder.Services.AddDbContext<OrderContext>();

// ===== Repositories =====
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IOrderingRepository, OrderingRepository>();

// ===== Application Services =====
builder.Services.AddApplicationService();

// ===== AutoMapper =====
builder.Services.AddAutoMapper(typeof(GeneralMapping)); // GeneralMapping Profile'ýný ekledik

// ===== MediatR =====
builder.Services.AddMediatR(cfg =>
{
    // Assembly yerine handler’lardan bir tip veriyoruz
    cfg.RegisterServicesFromAssemblyContaining<CreateOrderDetailCommand>();
    cfg.RegisterServicesFromAssemblyContaining<GetOrderDetailsQuery>();
    cfg.RegisterServicesFromAssemblyContaining<CreateAddressCommand>();
    cfg.RegisterServicesFromAssemblyContaining<GetAddressesQuery>();
});// Bu sayede tüm IRequest/IRequestHandler sýnýflarý otomatik bulunur
// Artýk handler'larý tek tek eklemene gerek yok

// ===== Controllers =====
builder.Services.AddControllers();

// ===== Swagger =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
