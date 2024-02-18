using EticaretApp.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.AuthorizationEndPoints.AssigneRole
{
    public class AssigneRoleCommandHandler : IRequestHandler<AssigneRoleCommandRequest, AssigneRoleCommandResponse>
    {
        private readonly IAuthorizationEndpointService _authorizationEndpointService;

        public AssigneRoleCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<AssigneRoleCommandResponse> Handle(AssigneRoleCommandRequest request, CancellationToken cancellationToken)
        {
           await _authorizationEndpointService.AssignRoleEndpointAsync(request.Roles, request.Menu, request.Code,request.Type);

            return new();
        }
    }
}
