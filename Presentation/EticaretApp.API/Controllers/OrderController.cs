using EticaretApp.Application.Features.Commands.Order.CreateOrder;
using EticaretApp.Application.Features.Queries.Order;
using EticaretApp.Application.Features.Queries.Order.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllOrder([FromQuery]GetAllOrderQueryRequest getAllOrderQueryRequest)
        {
            GetAllOrderQueryResponse response = await _mediator.Send(getAllOrderQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest byIdQueryRequest)
        {
            GetOrderByIdQueryResponse response = await _mediator.Send(byIdQueryRequest);
            return Ok(response);
        }
    }
}
