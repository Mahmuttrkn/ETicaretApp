using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.Repositories;
using EticaretApp.Application.ViewModuls.Basket;
using EticaretApp.Domain.Entities;
using EticaretApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketWriterRepository _basketWriterRepository;

        public BasketService(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriterRepository basketWriterRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriterRepository = basketWriterRepository;
        }

        private async Task<Basket?> ContexUser()
        {
            var username = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            if(!string.IsNullOrEmpty(username))
            {
               AppUser? user = await _userManager.Users.Include(u => u.Baskets)
                     .FirstOrDefaultAsync(u => u.UserName == username);


                var _basket = from basket in user?.Baskets
                              join order in _orderReadRepository.Table
                              on basket.Id equals order.Id into BasketOrders
                              from order in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                 Basket = basket,
                                 Order = order
                              };
                Basket? targetBasket = null;
                if(_basket.Any(b => b.Order is null))
                {
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                }
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }
               await _basketWriterRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Kullanıcı Sepetinde Hata Oluştu");
        }

        public Task AddItemToBasketAsync(VM_Create_Basket_Item basketItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBasketItemAsync(string basketItemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BasketItem>> GetAllBasketItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateBasketItemAsync(VM_Update_Basket_Item updateBasketItem)
        {
            throw new NotImplementedException();
        }
    }
}
