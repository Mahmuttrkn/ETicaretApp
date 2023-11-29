using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.Abstractions.Token;
using EticaretApp.Application.DTO_s;
using EticaretApp.Application.DTO_s.User;
using EticaretApp.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
          var token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password);
            return new LoginUsersSuccessCommandlResponse()
            {
                Token = token,
            };
        }
    }
}
