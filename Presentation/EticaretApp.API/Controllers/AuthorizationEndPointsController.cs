using EticaretApp.Application.Features.Commands.AuthorizationEndPoints.AssigneRole;
using EticaretApp.Application.Features.Queries.Role.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndPointsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationEndPointsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("action")]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
        {
          GetRolesToEndpointQueryResponse response =  await _mediator.Send(getRolesToEndpointQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssigneRole(AssigneRoleCommandRequest assigneRoleCommandRequest)
        {
            assigneRoleCommandRequest.Type = typeof(Program);
         AssigneRoleCommandResponse response =  await _mediator.Send(assigneRoleCommandRequest);
            return Ok(response);
        }
    }
}
