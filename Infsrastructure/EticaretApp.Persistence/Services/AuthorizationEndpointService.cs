using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.Abstractions.Services.Configurations;
using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities;
using EticaretApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        private readonly IApplicationService _applicationService;
        private readonly IEndpointReadRepository _endpointReadRepository;
        private readonly IEndpointWriterRepository _endpointWriterRepository;
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly IMenuWriterRepository _menuWriterRepository;
        private readonly RoleManager<AppRole> _roleManager;

        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepository endpointReadRepository, IEndpointWriterRepository endpointWriterRepository, IMenuReadRepository menuReadRepository, IMenuWriterRepository menuWriterRepository, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriterRepository = endpointWriterRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriterRepository = menuWriterRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] rolesName,string menu, string code, Type type)
        {
            Menu? _menu = await _menuReadRepository.GetSingleAsync(e => e.Name == menu);
            if (_menu == null)
            {
               await _menuWriterRepository.AddAsync(new()
               {
                   Id = Guid.NewGuid(),
                   Name = menu,
               });
            }
           await _menuWriterRepository.SaveAsync();

            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).FirstOrDefaultAsync(m => m.Code == code && m.Menu.Name == menu);
            if (endpoint == null)
            {
               var action = _applicationService.GetAuthorizeDefinitionEndPoint(type).FirstOrDefault(m => m.Name == menu)?.Actions.FirstOrDefault(a => a.Code == code);


                endpoint = new()
                {
                    ActionType = action.ActionType,
                    Code = code,
                    Id = Guid.NewGuid(),
                    HttpType = action.HttpType,
                    Definition = action.Definition
                };

                await _endpointWriterRepository.AddAsync(endpoint);
                await _endpointWriterRepository.SaveAsync();
            }

           var appRoles =  await _roleManager.Roles.Where(r => rolesName.Contains(r.Name)).ToListAsync();

           foreach (var role in appRoles)
            {
                endpoint.Roles.Add(role);
            }
            await _endpointWriterRepository.SaveAsync();
        }
    }
}
