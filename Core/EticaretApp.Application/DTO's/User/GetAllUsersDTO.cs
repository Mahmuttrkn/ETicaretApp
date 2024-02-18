using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.DTO_s.User
{
    public class GetAllUsersDTO
    {
        public string Id { get; set; }
        public string NameSurname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
