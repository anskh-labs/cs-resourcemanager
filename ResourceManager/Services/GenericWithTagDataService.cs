namespace ResourceManager.Services
{
    //public class GenericWithTagsDataService<T> : GenericDataService<T> where T : BaseEntity
    //{
    //    public GenericWithTagsDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory) : base(contextFactory)
    //    {

    //    }

    //    public async override Task<T> Create(T entity)
    //    {
    //        ICollection<Tag> tags = new Collection<Tag>();

    //        if (entity.Tags != null)
    //        {
    //            tags = entity.Tags;
    //        }

    //        entity.Tags = new Collection<Tag>();

    //        using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
    //        {
    //            if (tags.Count > 0)
    //            {
    //                foreach (Tag tag in tags)
    //                {
    //                    Tag tagToAdd = await db.Tags.FirstAsync(t => t.Equals(tag));
    //                    entity.Tags.Add(tagToAdd);
    //                }
    //            }
    //            var createdResult = await db.Set<T>().AddAsync(entity);
    //            await db.SaveChangesAsync();

    //            return createdResult.Entity;
    //        }
    //    }

    //    public async override Task<T> Update(int id, T entity)
    //    {
    //        entity.ID = id;
    //        ICollection<Tag> tags = new Collection<Tag>();

    //        if (entity.Tags != null)
    //        {
    //            tags = entity.Tags;
    //        }

    //        entity.Tags = new Collection<Tag>();

    //        RemoveTagsFromEntity(id);

    //        using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
    //        {
    //            if (tags.Count > 0)
    //            {
    //                foreach (Tag tag in tags)
    //                {
    //                    Tag tagToAdd = await db.Tags.FirstAsync(t => t.ID == tag.ID);
    //                    entity.Tags.Add(tagToAdd);
    //                }
    //            }
    //            db.Set<T>().Update(entity);
    //            await db.SaveChangesAsync();

    //            return entity;
    //        }
    //    }

    //    public async override Task<T> Get(int id)
    //    {
    //        using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
    //        {
    //            T entity = await db.Set<T>().Include(t => t.Tags).FirstAsync(e => e.ID == id);
    //            return entity;
    //        }
    //    }
    //    public async override Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate)
    //    {
    //        using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
    //        {
    //            T? entity = await db.Set<T>().Include(t=>t.Tags).FirstOrDefaultAsync(predicate);
    //            return entity;
    //        }
    //    }
    //    public async override Task<IList<T>> GetAll()
    //    {
    //        using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
    //        {
    //            IList<T> entities = await db.Set<T>().Include(t => t.Tags).ToListAsync();
    //            return entities;
    //        }
    //    }
    //    public override async Task<IList<T>> GetWhere(Expression<Func<T, bool>> predicate)
    //    {
    //        using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
    //        {
    //            IList<T> list = await db.Set<T>().Include(t=>t.Tags).Where(predicate).ToListAsync();
    //            return list;
    //        }
    //    }
    //    public async override Task<bool> Delete(int id)
    //    {
    //        RemoveTagsFromEntity(id);

    //        using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
    //        {
    //            T? entity = await db.Set<T>().FirstOrDefaultAsync((e) => e.ID == id);
    //            if (entity != null)
    //            {
    //                db.Set<T>().Remove(entity);
    //                await db.SaveChangesAsync();

    //                return true;
    //            }

    //            return false;
    //        }
    //    }

    //    private async void RemoveTagsFromEntity(int id)
    //    {
    //        using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
    //        {
    //            T entityTagRemoval = await db.Set<T>().Include(t => t.Tags).FirstAsync(t => t.ID == id);

    //            if (entityTagRemoval.Tags.Count != 0)
    //            {
    //                foreach (Tag tag in entityTagRemoval.Tags.ToList())
    //                {
    //                    Tag tagToRemove = await db.Tags.FirstAsync(T => T.ID == tag.ID);
    //                    entityTagRemoval.Tags.Remove(tagToRemove);
    //                }
    //                await db.SaveChangesAsync();
    //            }
    //        }
    //    }
    //}
}
