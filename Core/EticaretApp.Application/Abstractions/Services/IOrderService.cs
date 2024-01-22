using EticaretApp.Application.DTO_s.Order;
using EticaretApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDTO createOrderDTO);
        Task<ListOrderDTO> GetAllOrderAsync(int page,int size);
        Task<SingleOrderDTO> GetOrderByIdAsync(string id);
        public Task UpdateOrder(UpdateOrderDTO updateOrderDTO);
        public Task DeleteOrder(string userId);
      
        public Task CompleteOrderAsync(string id);
    }
}
