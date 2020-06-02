using Cubes3DIntersection.Core.Entities.Base;
using Cubes3DIntersection.Core.Repository;
using Cubes3DIntersection.Infrastructure.Data;
using Cubes3DIntersection.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cubes3DIntersection.Infrastructure.Repository
{
    public class Repository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly Cube3DIntersectionDbContext _dbContext;

        public Repository(Cube3DIntersectionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(T entity)
        {
            _dbContext.DetachLocal(entity, entity.Id);
            await _dbContext.SaveChangesAsync();
        }
    }
}