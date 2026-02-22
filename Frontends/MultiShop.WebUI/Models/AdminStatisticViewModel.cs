namespace MultiShop.WebUI.Models
{
    public class AdminStatisticViewModel
    {
        public long BrandCount { get; set; }
        public long ProductCount { get; set; }
        public long CategoryCount { get; set; }

        public string MaxPriceProductName { get; set; }
        public string MinPriceProductName { get; set; }

        public int UserCount { get; set; }

        public int TotalCommentCount { get; set; }
        public int ActiveCommentCount { get; set; }
        public int PassiveCommentCount { get; set; }

        public int DiscountCouponCount { get; set; }
        public int MessageTotalCount { get; set; }
    }
}
