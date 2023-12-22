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
        public async Task<IActionResult> GetBasketItems([FromQuery]GetAllBasketItemQueriyRequest getAllBasketItemQueriyRequest)
        {
            List<GetAllBasketItemQueriyResponse> response = await _mediator.Send(getAllBasketItemQueriyRequest);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddBasketItems(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateQuantityToBasket(UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
            UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{basketItemId}")]
        public async Task<IActionResult> DeleteBasketItems([FromRoute]DeleteItemToBasketCommandRequest deleteItemToBasketCommandRequest)
        {
            DeleteItemToBasketCommandResponse response = await _mediator.Send(deleteItemToBasketCommandRequest);
            return Ok(response);
        }
    }
}
