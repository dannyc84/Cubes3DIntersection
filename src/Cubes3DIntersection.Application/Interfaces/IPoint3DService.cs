using System.Collections.Generic;
using System.Threading.Tasks;
using Cubes3DIntersection.Application.Models;

namespace Cubes3DIntersection.Application.Interfaces
{
    public interface IPoint3DService
    {
        Task<IEnumerable<Point3DModel>> GetPoints3DList();

        Task<Point3DModel> GetPointById(int point3DId);

        Task<Point3DModel> Create(Point3DModel point3D);

        Task Update(Point3DModel pointCoordinates);
    }
}