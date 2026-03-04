using MultiShop.RapidApiWebUI.Models;

namespace MultiShop.RapidApiWebUI.Services.FinanceServices
{
    public interface IFinanceService
    {
        Task<CurrencyViewModel> GetExchangeRateAsync(string from, string to);
    }
}
