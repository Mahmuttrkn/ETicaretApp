using EticaretApp.Application.DTO_s.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task<DTO_s.Token> LoginAsync(string UserNameOrEmail,string Password);
        Task<DTO_s.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
