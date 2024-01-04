using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.DTO_s.Order;
using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities;
using EticaretApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
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
            await _orderWriterRepository.AddAsync(new()
            {
                Address = createOrderDTO.Address,
                Description = createOrderDTO.Description,
                Id = Guid.Parse(createOrderDTO.BasketId)
            });
           await _orderWriterRepository.SaveAsync();
        }

        public Task DeleteOrder(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrder()
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder(UpdateOrderDTO updateOrderDTO)
        {
            throw new NotImplementedException();
        }
    }
}
