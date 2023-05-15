using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManager.Services
{
    public class RoleDataService : GenericDataService<Role>, IRoleDataService
    {
        public RoleDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public async Task<IList<string>> GetRoleStringForUserID(int id)
        {
            using(ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from r in db.Roles
                                     from ur in db.UserRoles.Where(ur => ur.RoleID == r.ID && ur.UserID == id)
                                     select r.Name)
                                     .ToListAsync();
                return results;
            }
        }
        public async Task<IList<Role>> GetRoleObjectForUserID(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from r in db.Roles
                                     from ur in db.UserRoles.Where(ur => ur.RoleID == r.ID && ur.UserID == id)
                                     select r)
                                     .OrderBy(x=>x.Name)
                                     .ToListAsync();
                return results;
            }
        }
        public async Task<int> UpdatePermissions(Role role, IList<Permission> perms)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var r = db.Roles.Include(x => x.RolePermissions).Single(r => r.ID == role.ID); 
                db.UpdateManyToMany(r.RolePermissions, perms.Select(x => new RolePermission { PermissionID = x.ID, RoleID = r.ID }), x => x.PermissionID);

                return await db.SaveChangesAsync();
            }
        }
    }
}
