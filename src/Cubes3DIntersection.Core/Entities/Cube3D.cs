using Cubes3DIntersection.Core.Entities.Base;
using System.Collections.Generic;

namespace Cubes3DIntersection.Core.Entities
{
    public class Cube3D : Entity
    {
        public ICollection<Cube3DIntersection> Cube3DIntersection { get; set; }

        public Point3D PointCoordinates { get; set; }

        public ICollection<Edge> Edges { get; set; }

        public Cube3D()
        {
            Cube3DIntersection = new HashSet<Cube3DIntersection>();
            Edges = new HashSet<Edge>();
        }

        public static Cube3D Create(int cubeId)
        {
            return new Cube3D
            {
                Id = cubeId
            };
        }
    }
}