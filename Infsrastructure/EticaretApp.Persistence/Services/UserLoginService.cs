using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.Abstractions.Token;
using EticaretApp.Application.DTO_s;
using EticaretApp.Application.DTO_s.User;
using EticaretApp.Application.Exceptions;
using EticaretApp.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace EticaretApp.Persistence.Services
{
    public class UserLoginService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<EticaretApp.Domain.Entities.Identity.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly SignInManager<EticaretApp.Domain.Entities.Identity.AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public UserLoginService(IConfiguration configuration, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IUserService userService, IMailService mailService)
        {
            _configuration = configuration;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<Token> GoogleLoginAsync(string IdToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Audience"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(IdToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            EticaretApp.Domain.Entities.Identity.AppUser appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = appUser != null;
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(payload.Email);
                if (appUser == null)
                {
                    appUser = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        NameSurname = payload.Name,
                        UserName = payload.Email
                    };
                    var identityResult = await _userManager.CreateAsync(appUser);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(appUser, info);//AspNetUsers tablosuna kullanıcı ekliyoruz.
            }
            else
            {
                throw new Exception("Invalid external authentication");
            }
            Token token = _tokenHandler.CreateAccessToken(appUser);
           await _userService.UpdateRefreshToken(token.RefreshToken, appUser, token.Expiration,10);
            return token;

        }

        public async Task<Token> LoginAsync(string UserNameOrEmail, string Password)
        {
            EticaretApp.Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(UserNameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(UserNameOrEmail);
            }
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, Password,false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10);
                return token;
            }
            throw new AuthenticationErrorException();
        }

        public async Task PasswordResetAsync(string email)
        {
          AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
              string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user); //Reset tokenı burada oluşturuyoruz.
               
                byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
               resetToken = WebEncoders.Base64UrlEncode(tokenBytes); //URL üzerinde taşınabilir bir veri formatına getirdik.(Şifreleme işlemi gerçekleştirdik.)

               await _mailService.SendPasswordResetMailAsync(email,user.Id,resetToken);
            }
           
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
           AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if(user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
               Token token = _tokenHandler.CreateAccessToken(user);
               await _userService.UpdateRefreshToken(refreshToken,user,token.Expiration,10);
                return token;
            }
            else { throw new NotFoundUserException(); }

        }
    }
}
