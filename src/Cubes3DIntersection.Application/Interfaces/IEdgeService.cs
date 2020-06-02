using Cubes3DIntersection.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cubes3DIntersection.Application.Services
{
    public interface IEdgeService
    {
        Task<EdgeModel> Create(EdgeModel edge);

        Task<IEnumerable<EdgeModel>> GetCubes3DList();

        Task<EdgeModel> GetEdgeById(int cube3DId);
    }
}