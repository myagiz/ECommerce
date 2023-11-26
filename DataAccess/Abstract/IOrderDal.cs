using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderDal
    {
        Task CreateOrderAsync(CreateOrderDto model);
        Task<List<GetAllOrderDto>> GetAllOwnedOrderAsync();
        Task<List<GetAllOrderDto>> GetAllOrderAsync();
        Task<GetAllOrderDto> GetOrderDetailAsync(int orderId);
    }
}
