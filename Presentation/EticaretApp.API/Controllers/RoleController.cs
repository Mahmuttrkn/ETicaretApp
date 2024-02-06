using EticaretApp.Application.CustomAttribute;
using EticaretApp.Application.Enums;
using EticaretApp.Application.Features.Commands.Role.CreateRole;
using EticaretApp.Application.Features.Commands.Role.DeleteRole;
using EticaretApp.Application.Features.Commands.Role.UpdateRole;
using EticaretApp.Application.Features.Queries.Role.GetRoles;
using EticaretApp.Application.Features.Queries.Role.GetRolesById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu ="Roles",Definition ="Get All Roles",ActionType = ActionType.Reading)]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueryRequest getRolesQueryRequest)
        {
           GetRolesQueryResponse response = await _mediator.Send(getRolesQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = "Roles", Definition = "Get Roles By Id", ActionType = ActionType.Reading)]
        public async Task<IActionResult> GetRoles([FromRoute] GetRolesByIdQueryRequest getRolesByIdQueryRequest)
        {
           GetRolesByIdQueryResponse response = await _mediator.Send(getRolesByIdQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = "Roles", Definition = "Create Roles", ActionType = ActionType.Writing)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
         CreateRoleCommandResponse response = await _mediator.Send(createRoleCommandRequest);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        [AuthorizeDefinition(Menu = "Roles", Definition = "Update Roles", ActionType = ActionType.Updating)]
        public async Task<IActionResult> UpdateRole([FromBody,FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
          UpdateRoleCommandResponse response =  await _mediator.Send(updateRoleCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(Menu = "Roles", Definition = "Delete Roles", ActionType = ActionType.Deleting)]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(deleteRoleCommandRequest);
            return Ok(response);
        }
    }
}
