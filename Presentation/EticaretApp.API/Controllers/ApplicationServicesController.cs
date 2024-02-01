using EticaretApp.Application.Abstractions.Services.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationServicesController : ControllerBase
    {
       private readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        [HttpGet]
        public IActionResult GetAuthorizeDefinitionEndPoint()
        {
          var data =  _applicationService.GetAuthorizeDefinitionEndPoint(typeof(Program));
            return Ok(data);
        }
    }
}
