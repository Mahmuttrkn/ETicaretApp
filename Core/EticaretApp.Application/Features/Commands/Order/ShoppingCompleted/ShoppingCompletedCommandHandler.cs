using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.DTO_s.Order;
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
        private readonly IMailService _mailService;

        public ShoppingCompletedCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<ShoppingCompletedCommandResponse> Handle(ShoppingCompletedCommandRequest request, CancellationToken cancellationToken)
        {
            (bool succeeded, CompletedOrderSendMailDTO dto) result = await _orderService.CompleteOrderAsync(request.Id);
            if (result.succeeded)
            {
                await _mailService.SendCompletedOrderMailAsync(result.dto.Email,
                    result.dto.OrderCode,
                    result.dto.OrderDate,
                    result.dto.UserName,
                    result.dto.UserSurname);
            };
            return new();
        }
    }
}
