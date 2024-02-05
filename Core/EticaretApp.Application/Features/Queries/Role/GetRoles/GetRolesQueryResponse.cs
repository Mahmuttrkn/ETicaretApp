using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryResponse
    {
        public Dictionary<string,string> Datas { get; set; }
        public int TotalRoleCount { get; set; }
    }
}
