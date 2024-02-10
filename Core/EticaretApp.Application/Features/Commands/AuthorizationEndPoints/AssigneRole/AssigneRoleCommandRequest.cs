using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.AuthorizationEndPoints.AssigneRole
{
    public class AssigneRoleCommandRequest : IRequest<AssigneRoleCommandResponse>
    {
        public string[] rolesName { get; set; }
        public string Code { get; set; }
        public string Menu { get; set; }
        public Type? Type { get; set; }
    }
}
