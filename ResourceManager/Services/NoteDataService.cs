using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceManager.Services
{
    public class NoteDataService : GenericDataService<Note>, INoteDataService
    {
        public NoteDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
        {
        }
        public override async Task<IList<Note>> GetAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<Note> entities = await db.Notes.Include(x => x.NoteTags).ThenInclude(x => x.Tag).OrderByDescending(x => x.Date).ToListAsync();
                return entities;
            }
        }
        public async Task<int> UpdateTags(Note note, IList<Tag> tags)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var p = db.Notes.Include(x => x.NoteTags).Single(p => p.ID == note.ID);
                db.UpdateManyToMany(p.NoteTags, tags.Select(x => new NoteTag() { NoteID = note.ID, TagID = x.ID }), x => x.TagID);

                return await db.SaveChangesAsync();
            }
        }
    }
}
