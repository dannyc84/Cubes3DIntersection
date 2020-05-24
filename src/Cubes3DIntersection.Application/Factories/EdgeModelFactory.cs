using Cubes3DIntersection.Application.Models;

namespace Cubes3DIntersection.Application.Factories
{
    public static class EdgeModelFactory
    {
        public static EdgeModel Create(double point3DCenter, double edgeLength, int cube3DId) => new EdgeModel(point3DCenter, edgeLength, cube3DId);
    }
}
