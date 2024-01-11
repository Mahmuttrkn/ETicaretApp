using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.DTO_s.Order;
using EticaretApp.Application.Features.Queries.Order.GetOrderById;
using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities;
using EticaretApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriterRepository _orderWriterRepository;


        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriterRepository orderWriterRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriterRepository = orderWriterRepository;

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

        public async Task<ListOrderDTO> GetAllOrderAsync(int page,int size)
        {
            var query =  _orderReadRepository.Table.Include(o => o.Basket)
                  .ThenInclude(b => b.User)
                  .Include(o => o.Basket)
                  .ThenInclude(b => b.BasketItems)
                  .ThenInclude(bi => bi.Product);

            var data = query.Skip(page * size).Take(size);
               


            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data.Select(o => new
                {
                    Id = o.Id,
                    CreateDate = o.CreateDate,
                    Description = o.Description,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName
                }).ToListAsync()
            };
        }

        public async Task<SingleOrderDTO> GetOrderByIdAsync(string id)
        {
            var data = await _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {
                Id = data.Id.ToString(),
                Adress = data.Address,
                BasketItems = data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                CreatedDate = data.CreateDate,
                Description = data.Description,
                OrderCode = data.OrderCode,

            };
            
        }

        public Task UpdateOrder(UpdateOrderDTO updateOrderDTO)
        {
            throw new NotImplementedException();
        }


    }
}


