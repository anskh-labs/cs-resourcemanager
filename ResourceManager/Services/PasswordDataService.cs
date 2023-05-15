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
    public class PasswordDataService : GenericDataService<Password>, IPasswordDataService
    {
        public PasswordDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public override async Task<IList<Password>> GetAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<Password> entities = await db.Passwords.Include(x => x.PasswordTags).ThenInclude(x => x.Tag).OrderBy(x => x.Name).ToListAsync();
                return entities;
            }
        }
        public async Task<int> UpdateTags(Password pass, IList<Tag> tags)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var p = db.Passwords.Include(x => x.PasswordTags).Single(p => p.ID == pass.ID);
                db.UpdateManyToMany(p.PasswordTags, tags.Select(x => new PasswordTag() { PasswordID = pass.ID, TagID = x.ID }), x => x.TagID);

                return await db.SaveChangesAsync();
            }
        }
    }
}
