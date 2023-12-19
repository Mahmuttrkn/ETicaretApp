using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.ViewModuls.Basket;
using EticaretApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Services
{
    public class BasketService : IBasketService
    {
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
