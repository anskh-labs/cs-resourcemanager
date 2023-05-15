using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManager.Services
{
    public class PermissionDataService : GenericDataService<Permission>, IPermissionDataService
    {
        public PermissionDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public async Task<IList<string>> GetPermissionForRoles(IList<string> roles)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from p in db.Permissions
                                     from r in db.RolePermissions.Where(x=>roles.Contains(x.Role.Name) && x.PermissionID == p.ID)
                                     select p.Name).Distinct().ToListAsync();
                return results;
            }
            
        }
        public async Task<IList<Permission>> GetPermissionObjectForRoleID(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from p in db.Permissions
                                     from r in db.RolePermissions.Where(x => x.RoleID==id && x.PermissionID == p.ID)
                                     select p)
                                     .Distinct()
                                     .OrderBy(x=>x.Name)
                                     .ToListAsync();
                return results;
            }
        }
    }
}
