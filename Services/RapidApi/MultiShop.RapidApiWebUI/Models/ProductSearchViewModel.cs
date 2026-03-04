namespace MultiShop.RapidApiWebUI.Models
{
    public class ProductSearchViewModel
    {
        public string Query { get; set; } = "";
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public string SortBy { get; set; } = "BEST_MATCH";
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public List<ProductItemViewModel> Products { get; set; } = new();
        public List<FilterViewModel> Filters { get; set; } = new();
    }

    public class ProductItemViewModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ProductUrl { get; set; }
        public string Source { get; set; }
        public decimal? Price { get; set; }
        public string Currency { get; set; }
    }
}
