using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task<IResult> CreateOrderAsync(CreateOrderDto model);
        Task<IDataResult<List<GetAllOrderDto>>> GetAllOwnedOrderAsync();
        Task<IDataResult<List<GetAllOrderDto>>> GetAllOrderAsync();
        Task<IDataResult<GetAllOrderDto>> GetOrderDetailAsync(int orderId);

    }
}
