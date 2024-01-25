using EticaretApp.Application.Consts;
using EticaretApp.Application.CustomAttribute;
using EticaretApp.Application.Enums;
using EticaretApp.Application.Features.Commands.Order.CreateOrder;
using EticaretApp.Application.Features.Commands.Order.ShoppingCompleted;
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
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Order, ActionType = ActionType.Writing, Definition = "Create Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }
        
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Order, ActionType = ActionType.Reading, Definition = "Get Order")]
        public async Task<IActionResult> GetAllOrder([FromQuery]GetAllOrderQueryRequest getAllOrderQueryRequest)
        {
            GetAllOrderQueryResponse response = await _mediator.Send(getAllOrderQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Order, ActionType = ActionType.Reading, Definition = "Get Order By Id")]
        public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest byIdQueryRequest)
        {
            GetOrderByIdQueryResponse response = await _mediator.Send(byIdQueryRequest);
            return Ok(response);
        }
        [HttpGet("complete-order/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Order, ActionType = ActionType.Updating, Definition = "Shopping Completed Order")]
        public async Task<IActionResult> CompleteOrder([FromRoute] ShoppingCompletedCommandRequest shoppingCompletedCommandRequest)
        {
            ShoppingCompletedCommandResponse response = await _mediator.Send(shoppingCompletedCommandRequest);
            return Ok(response);
        }
    }
}
