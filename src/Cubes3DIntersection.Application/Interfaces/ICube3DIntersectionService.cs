using System.Collections.Generic;
using System.Threading.Tasks;
using Cubes3DIntersection.Application.Models;

namespace Cubes3DIntersection.Application.Interfaces
{
    public interface ICube3DIntersectionService
    {
        Task<Cube3DIntersectionModel> Create(Cube3DIntersectionModel Cube3DIntersectionModel);

        Task<Cube3DIntersectionModel> GetCube3DById(int cube3DId);

        Task<IEnumerable<Cube3DIntersectionModel>> GetCubes3DList();
    }
}