using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;

namespace ResourceManager.Services
{
    internal class RepositoryDataService : GenericDataService<Repository>, IRepositoryDataService
    {
        public RepositoryDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public override async Task<IList<Repository>> GetAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<Repository> entities = await db.Repositories.Include(x => x.RepositoryTags).ThenInclude(x => x.Tag).OrderBy(x => x.Title).ToListAsync();
                return entities;
            }
        }
        public async Task<int> UpdateTags(Repository repo, IList<Tag> tags)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var p = db.Repositories.Include(x => x.RepositoryTags).Single(p => p.ID == repo.ID);
                db.UpdateManyToMany(p.RepositoryTags, tags.Select(x => new RepositoryTag() { RepositoryID = repo.ID, TagID = x.ID }), x => x.TagID);

                return await db.SaveChangesAsync();
            }
        }
    }
}
