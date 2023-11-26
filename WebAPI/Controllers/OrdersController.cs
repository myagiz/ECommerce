using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrderAsync")]
        public async Task<IActionResult> GetAllOrderAsync()
        {
            var result = await _orderService.GetAllOrderAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("GetAllOwnedOrderAsync")]
        public async Task<IActionResult> GetAllOwnedOrderAsync()
        {
            var result = await _orderService.GetAllOwnedOrderAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("GetOrderDetailAsync/{orderId}")]
        public async Task<IActionResult> GetOrderDetailAsync(int orderId)
        {
            var result = await _orderService.GetOrderDetailAsync(orderId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("CreateOrderAsync")]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderDto model)
        {
            var result = await _orderService.CreateOrderAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
