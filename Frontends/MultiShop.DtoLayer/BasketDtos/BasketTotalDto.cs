using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.BasketDtos
{
    public class BasketTotalDto
    {
        public BasketTotalDto()
        {
            BasketItems = new List<BasketItemDto>();
        }

        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountRate { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }

        public decimal TotalPrice
            => BasketItems.Sum(x => x.Price * x.Quantity);
    }
}
