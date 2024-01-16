using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.DTO_s.User;
using EticaretApp.Application.Exceptions;
using EticaretApp.Application.Features.Commands.AppUser.CreateUser;
using EticaretApp.Application.Helpers;
using EticaretApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<EticaretApp.Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDTO> CreateAync(CreateUserDTO model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname,

            }, model.Password);

            CreateUserResponseDTO response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı Başarılı Şekilde Eklendi";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
           AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                //resetToken = Encoding.UTF8.GetString(tokenBytes);

                resetToken = resetToken.UrlDecode();
               IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                {
                   await _userManager.UpdateSecurityStampAsync(user);
                }
                else { throw new PasswordChangeFailedException(); }
            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser appUser, DateTime accessTokenDate, int refreshTokenLifeTime)
        {
             
             
            if(appUser != null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenEndDate = accessTokenDate.AddMinutes(refreshTokenLifeTime);
                await _userManager.UpdateAsync(appUser);
            }
            else
            {
                throw new NotFoundUserException();
            }
            
        }
    }
}
