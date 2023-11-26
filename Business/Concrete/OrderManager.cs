using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public async Task<IResult> CreateOrderAsync(CreateOrderDto model)
        {
            await _orderDal.CreateOrderAsync(model);
            return new SuccessResult(Messages.Added);
        }

        public async Task<IDataResult<List<GetAllOrderDto>>> GetAllOrderAsync()
        {
            var result = await _orderDal.GetAllOrderAsync();
            return new SuccessDataResult<List<GetAllOrderDto>>(result, Messages.Listed);
        }

        public async Task<IDataResult<List<GetAllOrderDto>>> GetAllOwnedOrderAsync()
        {
            var result = await _orderDal.GetAllOwnedOrderAsync();
            return new SuccessDataResult<List<GetAllOrderDto>>(result, Messages.Listed);
        }

        public async Task<IDataResult<GetAllOrderDto>> GetOrderDetailAsync(int orderId)
        {
            var result = await _orderDal.GetOrderDetailAsync(orderId);
            return new SuccessDataResult<GetAllOrderDto>(result, Messages.GetById);
        }
    }
}
