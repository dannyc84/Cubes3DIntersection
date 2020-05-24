using Cubes3DIntersection.Core.Entities.Base;
using Cubes3DIntersection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cubes3DIntersection.Infrastructure.Extensions
{
    public static class Cube3DIntersectionDbContextExtensions
    {
        public static void DetachLocal<T>(this Cube3DIntersectionDbContext context, T t, int entryId) where T : class, IEntity
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
