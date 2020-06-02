using System.Collections.Generic;
using System.Threading.Tasks;
using Cubes3DIntersection.Application.Models;

namespace Cubes3DIntersection.Application.Interfaces
{
    public interface IEdgeService
    {
        Task<EdgeModel> Create(EdgeModel edge);

        Task<IEnumerable<EdgeModel>> GetCubes3DList();

        Task<EdgeModel> GetEdgeById(int cube3DId);
    }
}