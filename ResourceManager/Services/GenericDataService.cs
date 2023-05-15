using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResourceManager.Services
{
    public class GenericDataService<T> : IDataService<T> where T : BaseEntity
    {
        protected IDesignTimeDbContextFactory<ResourceManagerDBContext> dbFactory;

        public GenericDataService(IDesignTimeDbContextFactory<ResourceManagerDBContext> contextFactory)
        {
            dbFactory = contextFactory;
        }

        public virtual async Task<int> CountAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                int count = await db.Set<T>().CountAsync();
                return count;
            }
        }

        public virtual async Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            using(ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                int count = await db.Set<T>().CountAsync(predicate);
                return count;
            }
        }

        public virtual async Task<T> Create(T entity)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                var createdResult = await db.Set<T>().AddAsync(entity);
                await db.SaveChangesAsync();
                
                return createdResult.Entity;
            }
        }

        public virtual async Task<bool> Delete(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                T? entity = await db.Set<T>().FirstOrDefaultAsync(e => e.ID == id);
                if (entity != null)
                {
                    db.Set<T>().Remove(entity);
                    await db.SaveChangesAsync();

                    return true;
                }

                return false;
            }
        }

        public virtual async Task<T?> SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                T? entity = await db.Set<T>().SingleOrDefaultAsync(predicate);
                return entity;
            }
        }

        public virtual async Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            using(ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                T? entity = await db.Set<T>().FirstOrDefaultAsync(predicate);
                return entity;
            }
        }

        public virtual async Task<T> Get(int id)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                T entity = await db.Set<T>().FirstAsync(e => e.ID == id);
                return entity;
            }
        }

        public virtual async Task<IList<T>> GetAll()
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<T> entities = await db.Set<T>().AsNoTracking().ToListAsync();
                return entities;
            }
        }

        public virtual async Task<IList<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            using(ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<T> list = await db.Set<T>().Where(predicate).ToListAsync();
                return list;
            }
        }

        public virtual async Task<IList<T>> GetWithRawSql(string query, params object[] parameters)
        {
            using(ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                IList<T> list = await db.Set<T>().FromSqlRaw(query, parameters).ToListAsync();
                return list;
            }
        }

        public virtual async Task<T> Update(int id, T entity)
        {
            using (ResourceManagerDBContext db = dbFactory.CreateDbContext(null!))
            {
                entity.ID = id;
                db.Set<T>().Update(entity);
                await db.SaveChangesAsync();

                return entity;
            }
        }
    }
}