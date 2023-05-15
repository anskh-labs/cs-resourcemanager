using ResourceManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface IUserDataService : IDataService<User>
    {
        Task<int> UpdateRoles(User user, IList<Role> roles);
    }
}