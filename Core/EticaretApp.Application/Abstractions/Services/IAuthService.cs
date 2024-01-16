using EticaretApp.Application.Abstractions.Services.Authentication;
using EticaretApp.Application.DTO_s.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Abstractions.Services
{
    public interface IAuthService: IExternalAuthentication, IInternalAuthentication
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken,string userId);
    }
}
