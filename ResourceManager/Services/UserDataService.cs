using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManager.Services
{
    public class UserDataService : GenericDataService<User>, IUserDataService
    {
        public UserDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public override async Task<IList<User>> GetAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<User> entities = await db.Users.OrderBy(x => x.Name).ToListAsync();
                return entities;
            }
        }
        public async Task<int> UpdateRoles(User user, IList<Role> roles)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var u = db.Users.Include(x => x.UserRoles).Single(u => u.ID == user.ID);
                db.UpdateManyToMany(u.UserRoles, roles.Select(x => new UserRole() { RoleID = x.ID, UserID = u.ID }), x=>x.RoleID);

                return await db.SaveChangesAsync();
            }
        }
    }
}
