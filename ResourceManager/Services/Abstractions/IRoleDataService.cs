using ResourceManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface IRoleDataService : IDataService<Role>
    {
        Task<IList<string>> GetRoleStringForUserID(int iD);
        Task<IList<Role>> GetRoleObjectForUserID(int iD);
        Task<int> UpdatePermissions(Role role, IList<Permission> permissions);
    }
}