using Microsoft.Extensions.Options;
using MultiShop.RapidApiWebUI.Models;
using System.Text.Json;

namespace MultiShop.RapidApiWebUI.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ProductApiOptions _options;

        public ProductService(HttpClient httpClient, IOptions<ProductApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<ProductSearchViewModel> SearchProductsAsync(ProductSearchViewModel model)
        {
            // Query oluştur
            var url = $"search-v2?q={Uri.EscapeDataString(model.Query)}&country=us&language=en&page={model.Page}&limit={model.Limit}&sort_by={model.SortBy}";

            if (model.MinPrice.HasValue)
                url += $"&min_price={model.MinPrice.Value}";
            if (model.MaxPrice.HasValue)
                url += $"&max_price={model.MaxPrice.Value}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("x-rapidapi-key", _options.ApiKey);
            request.Headers.Add("x-rapidapi-host", _options.Host);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json); // veya ILogger ile logla

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement.GetProperty("data");

            model.Products.Clear();
            if (root.TryGetProperty("products", out var products))
            {
                foreach (var p in products.EnumerateArray())
                {
                    model.Products.Add(new ProductItemViewModel
                    {
                        Title = p.GetProperty("product_title").GetString(),
                        ImageUrl = p.GetProperty("product_photos")[0].GetString(),
                        ProductUrl = p.GetProperty("product_page_url").GetString(),
                        Source = "RapidAPI",
                        Price = p.TryGetProperty("offer", out var offerEl) && offerEl.TryGetProperty("price", out var priceEl)
                                ? Decimal.Parse(priceEl.GetString().Trim('$'))
                                : (decimal?)null,
                        Currency = "USD"
                    });
                }
            }

            // Filtreleri parse et
            model.Filters.Clear();
            if (root.TryGetProperty("filters", out var filters))
            {
                foreach (var f in filters.EnumerateArray())
                {
                    var filterVm = new FilterViewModel
                    {
                        Title = f.GetProperty("title").GetString(),
                        MultiValue = f.GetProperty("multivalue").GetBoolean()
                    };

                    foreach (var val in f.GetProperty("values").EnumerateArray())
                    {
                        filterVm.Values.Add(new FilterValueViewModel
                        {
                            Title = val.GetProperty("title").GetString(),
                            Query = val.GetProperty("q").GetString(),
                            ShopRs = val.GetProperty("shoprs").GetString()
                        });
                    }

                    model.Filters.Add(filterVm);
                }
            }

            return model;
        }
    }
}