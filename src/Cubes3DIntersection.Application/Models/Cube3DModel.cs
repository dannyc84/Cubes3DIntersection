using Cubes3DIntersection.Application.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cubes3DIntersection.Application.Models
{
    public class Cube3DModel : IModelBase
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public Point3DModel PointCoordinates { get; set; }

        [JsonIgnore]
        public IList<EdgeModel> Edges { get; set; }

        //public Cube3DModel(Point3DModel pointCoordinates, double edgeLength)
        //{
        //    PointCoordinates = pointCoordinates ?? throw new ArgumentNullException(nameof(pointCoordinates));
        //    Edges = new List<EdgeModel>
        //    {
        //        EdgeModelFactory.Create(pointCoordinates.X0, edgeLength, Id),
        //        EdgeModelFactory.Create(pointCoordinates.Y0, edgeLength, Id),
        //        EdgeModelFactory.Create(pointCoordinates.Z0, edgeLength, Id)
        //    };
        //}

        //public Cube3DModel()
        //{
        //}
    }
}
