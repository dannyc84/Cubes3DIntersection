using Cubes3DIntersection.Application.Models;

namespace Cubes3DIntersection.Application.Factories
{
    public static class PointModelFactory
    {
        public static Point3DModel Create(double x0, double y0, double z0) => new Point3DModel(x0, y0, z0);
    }
}