using EticaretApp.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.Order.ShoppingCompleted
{

    public class ShoppingCompletedCommandHandler : IRequestHandler<ShoppingCompletedCommandRequest, ShoppingCompletedCommandResponse>
    {
        private readonly IOrderService _orderService;

        public ShoppingCompletedCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<ShoppingCompletedCommandResponse> Handle(ShoppingCompletedCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CompleteOrderAsync(request.Id);

            return new();
        }
    }
}
