using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Models
{
    internal static class DbContextExtensions
    {
        public static void UpdateManyToMany<T, TKey>(
            this DbContext db,
            IEnumerable<T> dbEntries,
            IEnumerable<T> updatedEntries,
            Func<T, TKey> keyRetrievalFunction)
            where T : class
        {
            var oldItems = dbEntries.ToList();
            var newItems = updatedEntries.ToList();
            var toBeRemoved = oldItems.LeftComplementRight(newItems, keyRetrievalFunction);
            var toBeAdded = newItems.LeftComplementRight(oldItems, keyRetrievalFunction);
            var toBeUpdated = oldItems.Intersect(newItems, keyRetrievalFunction);

            db.Set<T>().RemoveRange(toBeRemoved);
            db.Set<T>().AddRange(toBeAdded);
            foreach (var entity in toBeUpdated)
            {
                var changed = newItems.Single(i => keyRetrievalFunction.Invoke(i)!.Equals(keyRetrievalFunction.Invoke(entity)));
                db.Entry(entity).CurrentValues.SetValues(changed);
            }
        }
        public static IEnumerable<T> LeftComplementRight<T, TKey>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            Func<T, TKey> keyRetrievalFunction)
        {
            var leftSet = left.ToList();
            var rightSet = right.ToList();

            var leftSetKeys = leftSet.Select(keyRetrievalFunction);
            var rightSetKeys = rightSet.Select(keyRetrievalFunction);

            var deltaKeys = leftSetKeys.Except(rightSetKeys);
            var leftComplementRightSet = leftSet.Where(i => deltaKeys.Contains(keyRetrievalFunction.Invoke(i)));
            
            return leftComplementRightSet;
        }

        public static IEnumerable<T> Intersect<T, TKey>(
            this IEnumerable<T> left,
            IEnumerable<T> right,
            Func<T, TKey> keyRetrievalFunction)
        {
            var leftSet = left.ToList();
            var rightSet = right.ToList();

            var leftSetKeys = leftSet.Select(keyRetrievalFunction);
            var rightSetKeys = rightSet.Select(keyRetrievalFunction);

            var intersectKeys = leftSetKeys.Intersect(rightSetKeys);
            var intersectionEntities = leftSet.Where(i => intersectKeys.Contains(keyRetrievalFunction.Invoke(i)));
            
            return intersectionEntities;
        }
    }
}
