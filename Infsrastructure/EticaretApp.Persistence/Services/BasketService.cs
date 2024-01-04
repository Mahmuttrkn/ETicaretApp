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
        private readonly IBasketItemWriterRepository _basketItemWriterRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly IBasketReadRepository _basketReadRepository;

        public BasketService(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriterRepository basketWriterRepository, IBasketItemWriterRepository basketItemWriterRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriterRepository = basketWriterRepository;
            _basketItemWriterRepository = basketItemWriterRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketReadRepository = basketReadRepository;
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
                    user?.Baskets.Add(targetBasket);
                }
               await _basketWriterRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Kullanıcı Sepetinde Hata Oluştu");
        }

        public async Task AddItemToBasketAsync(VM_Create_Basket_Item basketItem)
        {
            Basket? basket = await ContexUser();
            if (basket != null)
            {
               BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(basketItem.ProductId));

                if (_basketItem != null)
                {
                    _basketItem.Quantity++;
                }
                else
                {
                    await _basketItemWriterRepository.AddAsync(new()
                    {
                        BasketId=basket.Id,
                        ProductId=Guid.Parse(basketItem.ProductId),
                        Quantity=basketItem.Quantity
                    });

                   await _basketItemWriterRepository.SaveAsync();
                }
            }
        }

        public async Task DeleteBasketItemAsync(string basketItemId)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _basketItemWriterRepository.Remove(basketItem);
               await _basketItemWriterRepository.SaveAsync();
            }
            else
            {
                throw new Exception("Sepet boş");
            }
        }

        public async Task<List<BasketItem>> GetAllBasketItemsAsync()
        {
            Basket basket = await ContexUser();
            if (basket != null)
            {
               Basket? result = await _basketReadRepository.Table.Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product)
                    .FirstOrDefaultAsync(b => b.Id== basket.Id);

               return result.BasketItems.ToList();

            }
            else
            {
                throw new Exception("Kulanıcın Sepeti Çağırılırken Sorun Oluştu.");
            }
        }

        public async Task UpdateBasketItemAsync(VM_Update_Basket_Item updateBasketItem)
        {
            BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(updateBasketItem.BasketItemId);
            if (basketItem != null)
            {
               basketItem.Quantity = updateBasketItem.Quantity;
               await _basketItemWriterRepository.SaveAsync();
            }
            else {
                throw new Exception("Güncelleme hatası");
                    };
        }

        public Basket? GetUserActiveBasket
        {
            get
            {
                Basket? basket = ContexUser().Result; //O an ki kullanıcının sepetini elde edecez buradan.
                return basket;
            }
        }
    }
}
