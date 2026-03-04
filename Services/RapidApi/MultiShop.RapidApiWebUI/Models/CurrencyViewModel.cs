namespace MultiShop.RapidApiWebUI.Models
{
    public class CurrencyViewModel
    {
        public string Status { get; set; }
        public string RequestId { get; set; }

        public string FromSymbol { get; set; }
        public string ToSymbol { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal PreviousClose { get; set; }
        public DateTime LastUpdateUtc { get; set; }
    }
}
