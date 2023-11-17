using EticaretApp.Application.Abstractions.Token;
using EticaretApp.Application.DTO_s;
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
        private readonly UserManager<EticaretApp.Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<EticaretApp.Domain.Entities.Identity.AppUser> _singInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<EticaretApp.Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> singInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            EticaretApp.Domain.Entities.Identity.AppUser  user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            }
            if (user==null)
            {
                throw new NotFoundUserException();
            }
          SignInResult result = await _singInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
              EticaretApp.Application.DTO_s.Token token =  _tokenHandler.CreateAccessToken();
                return new LoginUsersSuccessCommandlResponse()
                {
                    Token = token
                };
            }
            throw new AuthenticationErrorException();

        }
    }
}
