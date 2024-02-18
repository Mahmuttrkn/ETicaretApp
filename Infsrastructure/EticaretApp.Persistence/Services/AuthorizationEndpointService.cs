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

        public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
        {
            Menu? _menu = await _menuReadRepository.GetSingleAsync(e => e.Name == menu);

            if (_menu == null)
            {
                _menu = new Menu()
                {
                    Id = Guid.NewGuid(),
                    Name = menu,
                };
                await _menuWriterRepository.AddAsync(_menu);
                await _menuWriterRepository.SaveAsync();
            }


            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(m => m.Code == code && m.Menu.Name == menu);
            if (endpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndPoint(type).FirstOrDefault(m => m.Name == menu)?.Actions.FirstOrDefault(a => a.Code == code);


                endpoint = new()
                {
                    ActionType = action.ActionType,
                    Code = code,
                    Id = Guid.NewGuid(),
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Menu = _menu
                };

                await _endpointWriterRepository.AddAsync(endpoint);
                await _endpointWriterRepository.SaveAsync();
            }

            foreach (var role in endpoint.Roles)
            {
                endpoint.Roles.Remove(role);
            }


            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
            {
                endpoint.Roles.Add(role);
            }
            await _endpointWriterRepository.SaveAsync();
        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
          Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .Include(e =>e.Menu)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if(endpoint != null)
            {
                return endpoint.Roles.Select(r => r.Name).ToList();
            }

            return null;

        }
    }
}
