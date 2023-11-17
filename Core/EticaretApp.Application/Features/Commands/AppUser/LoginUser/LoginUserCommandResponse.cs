using EticaretApp.Application.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        
        
    }
    public class LoginUsersSuccessCommandlResponse: LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
    public class LoginUsersErrorCommandlResponse: LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
