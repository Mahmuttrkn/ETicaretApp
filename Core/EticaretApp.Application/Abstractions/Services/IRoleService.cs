
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Abstractions.Services
{
    public interface IRoleService
    {
        (Object,int) GetAllRoles(int page, int size);
        public Task<(string id, string name)> GetRoleById(string id);
        public Task<bool> CreateRole(string name);
        public Task<bool> DeleteRole(string id);
        public Task<bool> UpdateRole(string name, string id);
    }
}
