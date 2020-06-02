using Cubes3DIntersection.Core.Entities.Base;

namespace Cubes3DIntersection.Core.Entities
{
    public class Cube3DIntersection : Entity
    {
        public Cube3D FirstCube3D { get; set; }

        public int SecondCube3DId { get; set; }

        public Cube3D SecondCube3D { get; set; }

        public double EdgesLength { get; set; }

        public bool? Collision { get; set; }

        public double? IntersectionVolume { get; set; }

        public Cube3DIntersection()
        {
        }

        public static Cube3DIntersection Create(int firstCube3DId, int secondCube3DId, double edgesLength, bool? collision = null, double? intersectionVolume = null)
        {
            return new Cube3DIntersection
            {
                Id = firstCube3DId,
                SecondCube3DId = secondCube3DId,
                EdgesLength = edgesLength,
                Collision = collision,
                IntersectionVolume = intersectionVolume
            };
        }
    }
}