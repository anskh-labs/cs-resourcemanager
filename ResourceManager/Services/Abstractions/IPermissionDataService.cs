using ResourceManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface IPermissionDataService : IDataService<Permission>
    {
        Task<IList<string>> GetPermissionForRoles(IList<string> roles);
        Task<IList<Permission>> GetPermissionObjectForRoleID(int iD);
    }
}