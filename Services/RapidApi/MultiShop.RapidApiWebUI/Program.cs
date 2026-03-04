using MultiShop.RapidApiWebUI.Services.FinanceServices;
using MultiShop.RapidApiWebUI.Services.WeatherServices;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<WeatherApiOptions>(
    builder.Configuration.GetSection("RapidApiWeather"));

builder.Services.Configure<FinanceApiOptions>(
    builder.Configuration.GetSection("RapidApiFinance"));

// 🔹 Weather service ekle
builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddScoped<IFinanceService, FinanceService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
