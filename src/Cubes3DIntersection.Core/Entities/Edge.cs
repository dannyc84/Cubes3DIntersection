using Cubes3DIntersection.Core.Entities.Base;

namespace Cubes3DIntersection.Core.Entities
{
    public class Edge : Entity, IEdgeLength
    {
        public double Point3DCenter { get; set; }

        public double Start { get; set; }

        public double End { get; set; }

        public double EdgesLength { get; set; }

        public int Cube3DId { get; set; }

        public Edge()
        {
        }

        public static Edge Create(int edgeId, int cube3DId, double point3DCenter, double edgesLength)
        {
            return new Edge
            {
                Id = edgeId,
                Cube3DId = cube3DId,
                Point3DCenter = point3DCenter,
                Start = point3DCenter - edgesLength / 2.0,
                End = point3DCenter + edgesLength / 2.0,
                EdgesLength = edgesLength,
            };
        }

    }
}
