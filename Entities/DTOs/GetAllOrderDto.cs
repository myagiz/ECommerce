using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class GetAllOrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public decimal TotalPrice { get; set; }
        public string CreateUserName { get; set; }
        public List<GetAllOrderDetailsDto> ProductList { get; set; }
    }

    public class GetAllOrderDetailsDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public decimal ProductTotalPrice { get; set; }
    }
}
