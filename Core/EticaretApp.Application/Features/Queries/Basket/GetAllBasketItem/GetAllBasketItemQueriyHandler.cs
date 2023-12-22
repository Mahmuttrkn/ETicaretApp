using EticaretApp.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Queries.Basket.GetAllBasketItem
{
    public class GetAllBasketItemQueriyHandler : IRequestHandler<GetAllBasketItemQueriyRequest, List<GetAllBasketItemQueriyResponse>>
    {
        private readonly IBasketService _basketService;

        public GetAllBasketItemQueriyHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetAllBasketItemQueriyResponse>> Handle(GetAllBasketItemQueriyRequest request, CancellationToken cancellationToken)
        {
          var basketItems =  await _basketService.GetAllBasketItemsAsync();

            return basketItems.Select(ba => new GetAllBasketItemQueriyResponse
            {
                BasketItemId = ba.Id.ToString(),
                Name = ba.Product.Name,
                Price = ba.Product.Price,
                Quantity = ba.Quantity
            }).ToList();
        }
    }
}
