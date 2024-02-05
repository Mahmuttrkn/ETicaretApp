using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.Role.UpdateRole
{
    public class UpdateRoleCommandRequest: IRequest<UpdateRoleCommandResponse>
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
