using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        EticaretApp.Application.DTO_s.Token CreateAccessToken();
    }
}
