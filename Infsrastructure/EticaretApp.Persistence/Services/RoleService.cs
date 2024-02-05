﻿using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string name)
        {
         IdentityResult? result =  await _roleManager.CreateAsync(new()
            {
             Id = Guid.NewGuid().ToString(),
                Name = name
            });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string name)
        {
         IdentityResult result =   await _roleManager.DeleteAsync(new() { Name = name});
            return result.Succeeded;
        }

        public Dictionary<string, string> GetAllRoles(int page,int size)
        {
           return _roleManager.Roles.Skip(page * size).Take(size).ToDictionary(role => role.Id,role => role.Name);
        }

        public async Task<(string id, string name)> GetRoleById(string id)
        {
          string role = await _roleManager.GetRoleIdAsync(new() { Id = id });
            return (id, role);
        }

        public async Task<bool> UpdateRole(string id,string name)
        {
           IdentityResult result = await _roleManager.UpdateAsync(new() { Id = id, Name = name });
            return result.Succeeded;
        }
    }
}
