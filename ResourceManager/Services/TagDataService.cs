using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Services
{
    public class TagDataService : GenericDataService<Tag>, ITagDataService
    {
        public TagDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public override async Task<IList<Tag>> GetAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<Tag> entities = await db.Tags
                    .Include(x=>x.PasswordTags)
                    .Include(x=>x.RepositoryTags)
                    .Include(x=>x.ActivityTags)
                    .Include(x=>x.ArticleTags)
                    .Include(x=>x.BookTags)
                    .AsNoTracking()
                    .OrderBy(x=>x.Name)
                    .ToListAsync();
                return entities;
            }
        }
        public override async Task<IList<Tag>> GetWhere(Expression<Func<Tag, bool>> predicate)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<Tag> entities = await db.Tags
                    .Include(x => x.PasswordTags)
                    .Include(x => x.RepositoryTags)
                    .Include(x => x.ActivityTags)
                    .Include(x => x.ArticleTags)
                    .Include(x => x.BookTags)
                    .Include(x => x.NoteTags)
                    .AsNoTracking()
                    .Where(predicate)
                    .ToListAsync();
                return entities;
            }
        }
        public async Task<IList<Tag>> GetTagObjectForPasswordID(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from t in db.Tags
                                     from ur in db.PasswordTags.Where(pt => pt.TagID == t.ID && pt.PasswordID == id)
                                     select t)
                                     .OrderBy(x=>x.Name)
                                     .ToListAsync();
                return results;
            }
        }
        public async Task<IList<Tag>> GetTagObjectForBookID(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from t in db.Tags
                                     from ur in db.BookTags.Where(pt => pt.TagID == t.ID && pt.BookID == id)
                                     select t)
                                     .OrderBy(x => x.Name)
                                     .ToListAsync();
                return results;
            }
        }
        public async Task<IList<Tag>> GetTagObjectForArticleID(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from t in db.Tags
                                     from ur in db.ArticleTags.Where(pt => pt.TagID == t.ID && pt.ArticleID == id)
                                     select t)
                                     .OrderBy(x => x.Name)
                                     .ToListAsync();
                return results;
            }
        }
        public async Task<IList<Tag>> GetTagObjectForActivityID(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from t in db.Tags
                                     from ur in db.ActivityTags.Where(pt => pt.TagID == t.ID && pt.ActivityID == id)
                                     select t)
                                     .OrderBy(x => x.Name)
                                     .ToListAsync();
                return results;
            }
        }
        public async Task<IList<Tag>> GetTagObjectForRepositoryID(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from t in db.Tags
                                     from ur in db.RepositoryTags.Where(pt => pt.TagID == t.ID && pt.RepositoryID == id)
                                     select t)
                                     .OrderBy(x => x.Name)
                                     .ToListAsync();
                return results;
            }
        }
        public async Task<IList<Tag>> GetTagObjectForNoteID(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var results = await (from t in db.Tags
                                     from ur in db.NoteTags.Where(pt => pt.TagID == t.ID && pt.NoteID == id)
                                     select t)
                                     .OrderBy(x => x.Name)
                                     .ToListAsync();
                return results;
            }
        }
    }
}
