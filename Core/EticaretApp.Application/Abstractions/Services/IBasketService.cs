using EticaretApp.Application.ViewModuls.Basket;
using EticaretApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetAllBasketItemsAsync();

        public Task AddItemToBasketAsync(VM_Create_Basket_Item basketItem);
        public Task UpdateBasketItemAsync(VM_Update_Basket_Item updateBasketItem);
        public Task DeleteBasketItemAsync(string basketItemId);
    }
}
