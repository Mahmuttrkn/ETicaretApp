using EticaretApp.Application.Consts;
using EticaretApp.Application.CustomAttribute;
using EticaretApp.Application.Enums;
using EticaretApp.Application.Features.Commands.Basket.AddItemToBasket;
using EticaretApp.Application.Features.Commands.Basket.DeleteItemToBasket;
using EticaretApp.Application.Features.Commands.Basket.UpdateQuantity;
using EticaretApp.Application.Features.Queries.Basket.GetAllBasketItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Basket, ActionType = ActionType.Reading,Definition ="Get Basket Item")]
        public async Task<IActionResult> GetBasketItems([FromQuery]GetAllBasketItemQueriyRequest getAllBasketItemQueriyRequest)
        {
            List<GetAllBasketItemQueriyResponse> response = await _mediator.Send(getAllBasketItemQueriyRequest);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Basket, ActionType = ActionType.Writing, Definition = "Add Basket Item")]
        public async Task<IActionResult> AddBasketItems(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Basket, ActionType = ActionType.Updating, Definition = "Update Basket Item")]
        public async Task<IActionResult> UpdateQuantityToBasket(UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
            UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{basketItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Basket, ActionType = ActionType.Deleting, Definition = "Delete Basket Item")]
        public async Task<IActionResult> DeleteBasketItems([FromRoute]DeleteItemToBasketCommandRequest deleteItemToBasketCommandRequest)
        {
            DeleteItemToBasketCommandResponse response = await _mediator.Send(deleteItemToBasketCommandRequest);
            return Ok(response);
        }
    }
}
