using EticaretApp.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        private readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
           var data = await _orderService.GetOrderByIdAsync(request.Id);
            return new()
            {
                Id = data.Id,
                OrderCode = data.OrderCode,
                BasketItems = data.BasketItems,
                CreatedDate = data.CreatedDate,
                Description = data.Description,
                Adress = data.Address,
                Completed = data.Completed
            };
        }
    }
}
