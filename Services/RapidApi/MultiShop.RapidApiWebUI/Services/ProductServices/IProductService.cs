using MultiShop.RapidApiWebUI.Models;

namespace MultiShop.RapidApiWebUI.Services.ProductServices
{
    public interface IProductService
    {
        Task<ProductSearchViewModel> SearchProductsAsync(ProductSearchViewModel model);
    }
}
