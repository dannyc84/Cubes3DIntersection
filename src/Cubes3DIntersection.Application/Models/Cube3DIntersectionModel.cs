using Cubes3DIntersection.Application.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cubes3DIntersection.Application.Models
{
    public class Cube3DIntersectionModel : IEdgeLengthModel, IModelBase
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public Cube3DModel FirstCube3D { get; set; }

        [JsonIgnore]
        public int SecondCube3DId { get; set; }

        [Required]
        public Cube3DModel SecondCube3D { get; set; }

        [Required(ErrorMessage = "Please enter a valid edges length")]
        public double EdgesLength { get; set; }

        public bool? Collision { get; set; }

        public double? IntersectionVolume { get; set; }

        public object Add(double value1, double value2, double value3, double value4)
        {
            throw new NotImplementedException();
        }
    }
}