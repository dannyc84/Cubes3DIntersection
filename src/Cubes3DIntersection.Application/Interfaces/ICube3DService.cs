using Cubes3DIntersection.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cubes3DIntersection.Application.Interfaces
{
    public interface ICube3DService
    {
        Task<Cube3DModel> Create(Cube3DModel Cube3DModel);
        Task<Cube3DModel> GetCube3DById(int cube3DId);
        Task<IEnumerable<Cube3DModel>> GetCubes3DList();
        Task Update(Cube3DModel Cube3DModel);
    }
}