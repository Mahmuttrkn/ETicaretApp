using EticaretApp.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Queries.Role.GetRolesById
{
    public class GetRolesByIdQueryHandler : IRequestHandler<GetRolesByIdQueryRequest, GetRolesByIdQueryResponse>
    {
        private readonly IRoleService _roleService;

        public GetRolesByIdQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRolesByIdQueryResponse> Handle(GetRolesByIdQueryRequest request, CancellationToken cancellationToken)
        {
          var result = await _roleService.GetRoleById(request.Id);

            return new()
            {
                Id = result.id,
                Name = result.name
            };
        }
    }
}
