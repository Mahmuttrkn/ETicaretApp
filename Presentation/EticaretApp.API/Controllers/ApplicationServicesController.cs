using EticaretApp.Application.Abstractions.Services.Configurations;
using EticaretApp.Application.Consts;
using EticaretApp.Application.CustomAttribute;
using EticaretApp.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ApplicationServicesController : ControllerBase
    {
       private readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        [HttpGet]
        [AuthorizeDefinition(Menu = "Application Services", ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints")]
        public IActionResult GetAuthorizeDefinitionEndPoint()
        {
          var data =  _applicationService.GetAuthorizeDefinitionEndPoint(typeof(Program));
            return Ok(data);
        }
    }
}
