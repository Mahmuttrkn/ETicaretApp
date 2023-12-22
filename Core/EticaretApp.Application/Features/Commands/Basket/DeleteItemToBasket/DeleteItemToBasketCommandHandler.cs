using EticaretApp.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.Basket.DeleteItemToBasket
{
    public class DeleteItemToBasketCommandHandler : IRequestHandler<DeleteItemToBasketCommandRequest, DeleteItemToBasketCommandResponse>
    {
        private readonly IBasketService _basketService;

        public DeleteItemToBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<DeleteItemToBasketCommandResponse> Handle(DeleteItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
           await _basketService.DeleteBasketItemAsync(request.basketItemId);

            return new();
        }
    }
}
