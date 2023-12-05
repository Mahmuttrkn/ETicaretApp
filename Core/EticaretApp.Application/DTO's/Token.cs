using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.DTO_s
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; } //Token Süresi

        public string RefreshToken { get; set; }
    }
}
