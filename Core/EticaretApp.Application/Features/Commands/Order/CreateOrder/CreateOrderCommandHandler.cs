﻿using EticaretApp.Application.Abstractions.Hubs;
using EticaretApp.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        private readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            
           await _orderService.CreateOrderAsync(new()
           {
               Address = request.Address,
               Description = request.Description,
               BasketId = _basketService.GetUserActiveBasket?.Id.ToString()
           });
           await _orderHubService.OrderAddedMessageAsync("Yeni sipariş oluşturulmuştur!!");

            return new();
        }
    }
}
