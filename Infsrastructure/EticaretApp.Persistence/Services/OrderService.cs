using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.DTO_s.Order;
using EticaretApp.Application.Features.Queries.Order.GetOrderById;
using EticaretApp.Application.Repositories;
using EticaretApp.Application.Repositories.CompletedOrder;
using EticaretApp.Domain.Entities;
using EticaretApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriterRepository _orderWriterRepository;
        private readonly ICompletedReadRepository _completedReadRepository;
        private readonly ICompletedWriterRepository _completedWriterRepository;


        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriterRepository orderWriterRepository, ICompletedReadRepository completedReadRepository, ICompletedWriterRepository completedWriterRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriterRepository = orderWriterRepository;
            _completedReadRepository = completedReadRepository;
            _completedWriterRepository = completedWriterRepository;
        }

        public async Task<(bool, CompletedOrderSendMailDTO)> CompleteOrderAsync(string id)
        {
            Order? order = await _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            if (order != null)
            {
                await _completedWriterRepository.AddAsync(new()
                {
                    OrderId = Guid.Parse(id)

                });
                return (await _completedWriterRepository.SaveAsync() > 0, new()
                {
                    OrderCode = order.OrderCode,
                    OrderDate = order.CreateDate,
                    UserName = order.Basket.User.UserName,
                    UserSurname = order.Basket.User.NameSurname,
                    Email = order.Basket.User.Email
                });
            }
            return (false,null);
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);

            await _orderWriterRepository.AddAsync(new()
            {
                Address = createOrderDTO.Address,
                Description = createOrderDTO.Description,
                Id = Guid.Parse(createOrderDTO.BasketId),
                OrderCode = orderCode

            });
            await _orderWriterRepository.SaveAsync();
        }

        public Task DeleteOrder(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ListOrderDTO> GetAllOrderAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                  .ThenInclude(b => b.User)
                  .Include(o => o.Basket)
                  .ThenInclude(b => b.BasketItems)
                  .ThenInclude(bi => bi.Product);



            var data = query.Skip(page * size).Take(size);


            var data2 = from order in data
                        join completedOrder in _completedReadRepository.Table
                       on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            Id = order.Id,
                            CreateDate = order.CreateDate,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co != null ? true : false
                        };



            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data2.Select(o => new
                {
                    Id = o.Id,
                    CreateDate = o.CreateDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    o.Completed
                }).ToListAsync()
            };
        }

        public async Task<SingleOrderDTO> GetOrderByIdAsync(string id)
        {
            var data = _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product);


            var data2 = await (from order in data
                               join completedOrder in _completedReadRepository.Table
                               on order.Id equals completedOrder.OrderId into co
                               from _co in co.DefaultIfEmpty()
                               select new
                               {
                                   Id = order.Id,
                                   CreateDate = order.CreateDate,
                                   OrderCode = order.OrderCode,
                                   Basket = order.Basket,
                                   Completed = _co != null ? true : false,
                                   Address = order.Address,
                                   Description = order.Description
                               }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id)); ;



            return new()
            {
                Id = data2.Id.ToString(),
                BasketItems = data2.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                CreatedDate = data2.CreateDate,
                Address = data2.Address,
                OrderCode = data2.OrderCode,
                Description = data2.Description,
                Completed = data2.Completed

            };

        }

        public Task UpdateOrder(UpdateOrderDTO updateOrderDTO)
        {
            throw new NotImplementedException();
        }


    }
}


