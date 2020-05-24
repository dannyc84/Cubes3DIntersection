using Cubes3DIntersection.Application.Models.Base;
using System.Text.Json.Serialization;

namespace Cubes3DIntersection.Application.Models
{
    public class EdgeModel : EdgeLengthModel, IModelBase
    {
        [JsonIgnore]
        public int Id { get; set; }

        public double Point3DCenter { get; set; }

        public double Start { get; set; }

        public double End { get; set; }

        public int Cube3DId { get; set; }

        public EdgeModel(double point3DCenter, double edgeLength, int cube3DId)
        {
            Point3DCenter = point3DCenter;
            Cube3DId = cube3DId;
            EdgesLength = edgeLength;
            Start = Point3DCenter - EdgesLength / 2.0;
            End = Point3DCenter + EdgesLength / 2.0;
        }

        public EdgeModel()
        {
        }
    }
}
