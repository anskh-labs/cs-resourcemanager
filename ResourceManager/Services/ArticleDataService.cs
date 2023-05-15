using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManager.Services
{
    public class ArticleDataService : GenericDataService<Article>, IArticleDataService
    {
        public ArticleDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public override async Task<IList<Article>> GetAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<Article> entities = await db.Articles.Include(x => x.ArticleTags).ThenInclude(x => x.Tag).OrderBy(x => x.Title).ToListAsync();
                return entities;
            }
        }
        public async Task<int> UpdateTags(Article article, IList<Tag> tags)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var p = db.Articles.Include(x => x.ArticleTags).Single(p => p.ID == article.ID);
                db.UpdateManyToMany(p.ArticleTags, tags.Select(x => new ArticleTag() { ArticleID = article.ID, TagID = x.ID }), x => x.TagID);

                return await db.SaveChangesAsync();
            }
        }
    }
}
