using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManager.Services
{
    public class BookDataService : GenericDataService<Book>, IBookDataService
    {
        public BookDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public override async Task<IList<Book>> GetAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<Book> entities = await db.Books.Include(x => x.BookTags).ThenInclude(x => x.Tag).OrderBy(x => x.Title).ToListAsync();
                return entities;
            }
        }
        public async Task<int> UpdateTags(Book book, IList<Tag> tags)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var p = db.Books.Include(x => x.BookTags).Single(p => p.ID == book.ID);
                db.UpdateManyToMany(p.BookTags, tags.Select(x => new BookTag() { BookID = book.ID, TagID = x.ID }), x => x.TagID);

                return await db.SaveChangesAsync();
            }
        }
    }
}
