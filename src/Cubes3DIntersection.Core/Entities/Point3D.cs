using Cubes3DIntersection.Core.Entities.Base;

namespace Cubes3DIntersection.Core.Entities
{
    public class Point3D : Entity
    {
        public double X0 { get; set; }

        public double Y0 { get; set; }

        public double Z0 { get; set; }

        public int Cube3DId { get; set; }

        public Cube3D Cube3D { get; set; }

        public Point3D()
        {
        }

        public static Point3D Create(int point3DId, int cube3DId, double x0, double y0, double z0)
        {
            return new Point3D
            {
                Id = point3DId,
                Cube3DId = cube3DId,
                X0 = x0,
                Y0 = y0,
                Z0 = z0
            };
        }
    }
}