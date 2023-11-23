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

        public GoogleLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
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
                }
            }
        }
    }
}
