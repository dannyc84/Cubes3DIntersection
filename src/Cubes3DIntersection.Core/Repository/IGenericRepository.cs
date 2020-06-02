using Cubes3DIntersection.Core.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cubes3DIntersection.Core.Repository
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<T> Create(T entity);

        Task Update(T entity);

        //void Delete(int id);
    }
}