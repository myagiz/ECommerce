using Core.Utilities.Current;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.DTOs;
using Entities.Entity;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreOrderDal : IOrderDal
    {
        public async Task CreateOrderAsync(CreateOrderDto model)
        {
            using (var context = new ECommerceDbContext())
            {
                Order order = new Order();
                order.UserId = UserCurrents.UserId();
                order.OrderStatus = Convert.ToInt32(OrderStatusTypesEnum.Waiting);
                order.OrderDate = DateTime.Now;
                order.AddressId = 1;
                order.CreateDate = DateTime.Now;
                order.CreateUserId = UserCurrents.UserId();
                order.IsActive = true;
                context.Orders.Add(order);
                await context.SaveChangesAsync();

                foreach (var item in model.ProductList)
                {
                    var getProductPrice = await context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                    if (getProductPrice != null && getProductPrice.UnitPrice != null && getProductPrice.UnitPrice != 0)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = order.Id;
                        orderDetail.ProductId = item.ProductId;
                        orderDetail.Amount = item.Amount;
                        orderDetail.TotalPrice = getProductPrice.UnitPrice * item.Amount;
                        orderDetail.CreateDate = DateTime.Now;
                        orderDetail.CreateUserId = UserCurrents.UserId();
                        orderDetail.IsActive = true;
                        context.OrderDetails.Add(orderDetail);
                        await context.SaveChangesAsync();
                    }

                }


            }
        }

        public async Task<List<GetAllOrderDto>> GetAllOrderAsync()
        {
            using (var context = new ECommerceDbContext())
            {
                var getOrder = await (
                    from a in context.Orders
                        .Where(x => x.IsActive == true)
                    join u in context.Users on a.UserId equals u.Id
                    select new GetAllOrderDto
                    {
                        Id = a.Id,
                        OrderDate = a.OrderDate,
                        CreateUserName = string.Concat(u.FirstName, " ", u.LastName," & ",u.EmailAddress),
                        OrderStatusId = a.OrderStatus,
                        OrderStatusName = a.OrderStatus == Convert.ToInt32(OrderStatusTypesEnum.Waiting) ? "Waiting" :
                            a.OrderStatus == Convert.ToInt32(OrderStatusTypesEnum.Cargo) ? "Cargo" :
                            a.OrderStatus == Convert.ToInt32(OrderStatusTypesEnum.Completed) ? "Completed" : "None",
                        TotalPrice = (
                            from b in context.OrderDetails
                                .Where(x => x.IsActive == true && x.OrderId == a.Id)
                            select b.TotalPrice
                        ).Sum(),
                        ProductList = (
                            from b in context.OrderDetails
                                .Where(x => x.IsActive == true && x.OrderId == a.Id)
                            join c in context.Products on b.ProductId equals c.Id
                            select new GetAllOrderDetailsDto
                            {
                                Id = b.Id,
                                OrderId = b.OrderId,
                                ProductId = b.ProductId,
                                ProductName = c.Name,
                                Amount = b.Amount,
                                ProductTotalPrice = b.TotalPrice,
                            }
                        ).ToList()
                    }
                ).OrderByDescending(x => x.OrderDate).ToListAsync();

                return getOrder;
            }
        }

        public async Task<List<GetAllOrderDto>> GetAllOwnedOrderAsync()
        {
            using (var context = new ECommerceDbContext())
            {
                var getOrder = await (
                    from a in context.Orders
                        .Where(x => x.IsActive == true && x.UserId == UserCurrents.UserId())
                    select new GetAllOrderDto
                    {
                        Id = a.Id,
                        OrderDate = a.OrderDate,
                        OrderStatusId = a.OrderStatus,
                        OrderStatusName = a.OrderStatus == Convert.ToInt32(OrderStatusTypesEnum.Waiting) ? "Waiting" :
                            a.OrderStatus == Convert.ToInt32(OrderStatusTypesEnum.Cargo) ? "Cargo" :
                            a.OrderStatus == Convert.ToInt32(OrderStatusTypesEnum.Completed) ? "Completed" : "None",
                        TotalPrice = (
                            from b in context.OrderDetails
                                .Where(x => x.IsActive == true && x.OrderId == a.Id)
                            select b.TotalPrice
                        ).Sum(),
                        ProductList = (
                            from b in context.OrderDetails
                                .Where(x => x.IsActive == true && x.OrderId == a.Id)
                            join c in context.Products on b.ProductId equals c.Id
                            select new GetAllOrderDetailsDto
                            {
                                Id = b.Id,
                                OrderId = b.OrderId,
                                ProductId = b.ProductId,
                                ProductName = c.Name,
                                Amount = b.Amount,
                                ProductTotalPrice = b.TotalPrice,
                            }
                        ).ToList()
                    }
                ).OrderByDescending(x => x.OrderDate).ToListAsync();

                return getOrder;
            }
        }

        public async Task<GetAllOrderDto> GetOrderDetailAsync(int orderId)
        {
            using (var context = new ECommerceDbContext())
            {
                var getOrder = await (
                    from a in context.Orders
                        .Where(x => x.IsActive == true && x.Id == orderId)
                    select new GetAllOrderDto
                    {
                        Id = a.Id,
                        OrderDate = a.OrderDate,
                        OrderStatusId = a.OrderStatus,
                        OrderStatusName = a.OrderStatus == Convert.ToInt32(OrderStatusTypesEnum.Waiting) ? "Waiting" :
                            a.OrderStatus == Convert.ToInt32(OrderStatusTypesEnum.Cargo) ? "Cargo" :
                            a.OrderStatus == Convert.ToInt32(OrderStatusTypesEnum.Completed) ? "Completed" : "None",
                        TotalPrice = (
                            from b in context.OrderDetails
                                .Where(x => x.IsActive == true && x.OrderId == a.Id)
                            select b.TotalPrice
                        ).Sum(),
                        ProductList = (
                            from b in context.OrderDetails
                                .Where(x => x.IsActive == true && x.OrderId == a.Id)
                            join c in context.Products on b.ProductId equals c.Id
                            select new GetAllOrderDetailsDto
                            {
                                Id = b.Id,
                                OrderId = b.OrderId,
                                ProductId = b.ProductId,
                                ProductName = c.Name,
                                Amount = b.Amount,
                                ProductTotalPrice = b.TotalPrice,
                            }
                        ).ToList()
                    }
                ).OrderByDescending(x => x.OrderDate).FirstOrDefaultAsync();

                return getOrder;
            }
        }
    }
}
