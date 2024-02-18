using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Queries.AppUser
{
    public class GetUserQueryResponse
    {
        public object Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}
