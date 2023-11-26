using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CreateOrderDto
    {
        public List<ProductListForOrderDto> ProductList { get; set; }
    }
    public class ProductListForOrderDto
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
