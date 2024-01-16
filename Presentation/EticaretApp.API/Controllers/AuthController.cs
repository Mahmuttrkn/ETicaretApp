using EticaretApp.Application.Features.Commands.AppUser.CreateUser;
using EticaretApp.Application.Features.Commands.AppUser.GoogleLogin;
using EticaretApp.Application.Features.Commands.AppUser.LoginUser;
using EticaretApp.Application.Features.Commands.AppUser.PasswordReset;
using EticaretApp.Application.Features.Commands.AppUser.UpdatePassword;
using EticaretApp.Application.Features.Commands.AppUser.verify_reset_token;
using EticaretApp.Application.Features.Commands.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody]RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody]PasswordResetCommandRequest passwordResetCommandRequest)
        {
           PasswordResetCommandResponse response = await _mediator.Send(passwordResetCommandRequest);
            return Ok(response);
        }
        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {
          VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
            return Ok(response);
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
          UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }
    }
}
