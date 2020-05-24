using System;
using Cubes3DIntersection.Application.Models;

namespace Cubes3DIntersection.Application.Extensions
{
    public static class EdgeModelExtensions
    {
        public static double Overlap(this EdgeModel firstEdge, EdgeModel secondEdge)
        {
            if (firstEdge is null)
            {
                throw new ArgumentNullException(nameof(firstEdge));
            }

            if (secondEdge is null)
            {
                throw new ArgumentNullException(nameof(secondEdge));
            }

            return Math.Max(0, firstEdge.Difference(secondEdge));
        }

        public static bool Collision(this EdgeModel firstEdge, EdgeModel secondEdge)
        {
            if (firstEdge is null)
            {
                throw new ArgumentNullException(nameof(firstEdge));
            }

            if (secondEdge is null)
            {
                throw new ArgumentNullException(nameof(secondEdge));
            }

            return firstEdge.Difference(secondEdge) >= 0;
        }

        private static double Difference(this EdgeModel firstEdge, EdgeModel secondEdge) =>
            Math.Min(secondEdge.End, firstEdge.End) - Math.Max(secondEdge.Start, firstEdge.Start);
    }
}
