using EticaretApp.Application.Abstractions.Token;
using EticaretApp.Application.DTO_s;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<EticaretApp.Domain.Entities.Identity.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "373473022397-6fvm2atr5gm9oj3f3on1sk56fbgslpnr.apps.googleusercontent.com" }
            };

           var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken,settings);

           var info = new UserLoginInfo(request.Provider, payload.Subject,request.Provider);
            EticaretApp.Domain.Entities.Identity.AppUser appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = appUser != null;
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(payload.Email);
                if(appUser==null)
                {
                    appUser = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        NameSurname = payload.Name,
                        UserName = payload.Email
                    };
                   var identityResult = await _userManager.CreateAsync(appUser);
                    result =identityResult.Succeeded;
                }
            }
            if(result)
            {
               await _userManager.AddLoginAsync(appUser, info);//AspNetUsers tablosuna kullanıcı ekliyoruz.
            }
            else
            {
                throw new Exception("Invalid external authentication");
            }
            Token token = _tokenHandler.CreateAccessToken();
            return new()
            {
                Token = token
            };
        }
    }
}
