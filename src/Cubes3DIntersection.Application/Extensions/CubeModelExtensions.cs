using Cubes3DIntersection.Application.Models;
using System.Linq;

namespace Cubes3DIntersection.Application.Extensions
{
    public static class CubeModelExtensions
    {
        public static double IntersectionVolume(this Cube3DModel firstCube, Cube3DModel secondCube) =>
                firstCube.Edges.ElementAt(0).Overlap(secondCube.Edges.ElementAt(0))
                * firstCube.Edges.ElementAt(1).Overlap(secondCube.Edges.ElementAt(1))
                * firstCube.Edges.ElementAt(2).Overlap(secondCube.Edges.ElementAt(2));

        public static bool Collision(this Cube3DModel firstCube, Cube3DModel secondCube) =>
                firstCube.Edges.ElementAt(0).Collision(secondCube.Edges.ElementAt(0))
                || firstCube.Edges.ElementAt(1).Collision(secondCube.Edges.ElementAt(1))
                || firstCube.Edges.ElementAt(2).Collision(secondCube.Edges.ElementAt(2));
    }
}
