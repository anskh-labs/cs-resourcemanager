using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManager.Services
{
    public class ActivityDataService : GenericDataService<Activity>, IActivityDataService
    {
        public ActivityDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public override async Task<IList<Activity>> GetAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<Activity> entities = await db.Activities.Include(x => x.ActivityTags).ThenInclude(x => x.Tag).OrderByDescending(x => x.Date).ToListAsync();
                return entities;
            }
        }
        public async Task<int> UpdateTags(Activity activity, IList<Tag> tags)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var p = db.Activities.Include(x => x.ActivityTags).Single(p => p.ID == activity.ID);
                db.UpdateManyToMany(p.ActivityTags, tags.Select(x => new ActivityTag() { ActivityID = activity.ID, TagID = x.ID }), x => x.TagID);

                return await db.SaveChangesAsync();
            }
        }
    }
}
