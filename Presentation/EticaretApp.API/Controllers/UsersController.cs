using EticaretApp.Application.CustomAttribute;
using EticaretApp.Application.Features.Commands.AppUser.CreateUser;
using EticaretApp.Application.Features.Commands.AppUser.GoogleLogin;
using EticaretApp.Application.Features.Commands.AppUser.LoginUser;
using EticaretApp.Application.Features.Queries.AppUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes ="Admin")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading,Definition ="Get User",Menu ="Get All User")]
        public async Task<IActionResult> GetAllUser(GetUserQueryRequest getUserQueryRequest)
        {
            GetUserQueryResponse response = await _mediator.Send(getUserQueryRequest);
            return Ok(response);
        }

    }

}

