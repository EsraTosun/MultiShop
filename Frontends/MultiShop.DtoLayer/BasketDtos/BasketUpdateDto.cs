using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.BasketDtos
{
    public class BasketUpdateDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
