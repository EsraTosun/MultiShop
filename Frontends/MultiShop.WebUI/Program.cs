using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;
using MultiShop.WebUI.Services.OrderServices.OrderOderingServices;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;
using MultiShop.WebUI.Services.UserIdentityServices;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

#region Configuration

builder.Services.Configure<ClientSettings>(
    builder.Configuration.GetSection("ClientSettings"));

builder.Services.Configure<ServiceApiSettings>(
    builder.Configuration.GetSection("ServiceApiSettings"));

var apiSettings = builder.Configuration
    .GetSection("ServiceApiSettings")
    .Get<ServiceApiSettings>()!;

#endregion

#region Authentication (COOKIE – WebUI)

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Login/Index/";
        opt.LogoutPath = "/Login/LogOut/";
        opt.AccessDeniedPath = "/Pages/AccessDenied/";
        opt.ExpireTimeSpan = TimeSpan.FromDays(5);
        opt.SlidingExpiration = true;
        opt.Cookie.Name = "MultiShopCookie";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Lax;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

#endregion

#region MVC & Core

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

#endregion

#region Token Handlers

builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

#endregion

#region Identity & Login

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddHttpClient<IIdentityService, IdentityService>(opt =>
{
    opt.BaseAddress = new Uri(apiSettings.IdentityServerUrl);
    opt.Timeout = TimeSpan.FromSeconds(30);
});

#endregion

#region CLIENT CREDENTIAL (PUBLIC / VISITOR)

builder.Services.AddHttpClient<ICategoryService, CategoryService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductService, ProductService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IBrandService, BrandService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IAboutService, AboutService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductImageService, ProductImageService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IFeatureSliderService, FeatureSliderService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IFeatureService, FeatureService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IOfferDiscountService, OfferDiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<ISpecialOfferService, SpecialOfferService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IContactService, ContactService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<ICommentService, CommentService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Comment.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

#endregion

#region STATISTIC SERVICES

builder.Services.AddHttpClient<ICatalogStatisticService, CatalogStatisticService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IUserStatisticService, UserStatisticService>(opt =>
{
    opt.BaseAddress = new Uri(apiSettings.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IDiscountStatisticService, DiscountStatisticService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Discount.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IMessageStatisticService, MessageStatisticService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Message.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

#endregion

#region RESOURCE OWNER PASSWORD (AUTH USER)

builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(apiSettings.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
{
    opt.BaseAddress = new Uri(apiSettings.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Basket.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderOderingService, OrderOderingService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderAddressService, OrderAddressService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICargoCompanyService, CargoCompanyService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Cargo.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICargoCustomerService, CargoCustomerService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Cargo.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IDiscountService, DiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Discount.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IMessageService, MessageService>(opt =>
{
    opt.BaseAddress = new Uri($"{apiSettings.OcelotUrl}/{apiSettings.Message.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

#endregion

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});

builder.Services.AddControllersWithViews()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

#region Pipeline

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

var supportedCultures = new[] { "tr", "en", "fr", "de", "it" };

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("tr")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion
