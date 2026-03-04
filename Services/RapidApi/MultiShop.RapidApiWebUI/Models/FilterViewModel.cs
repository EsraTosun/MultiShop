namespace MultiShop.RapidApiWebUI.Models
{
    public class FilterViewModel
    {
        public string Title { get; set; }
        public bool MultiValue { get; set; }
        public List<FilterValueViewModel> Values { get; set; } = new();
    }

    public class FilterValueViewModel
    {
        public string Title { get; set; }
        public string Query { get; set; }
        public string ShopRs { get; set; }
    }
}
