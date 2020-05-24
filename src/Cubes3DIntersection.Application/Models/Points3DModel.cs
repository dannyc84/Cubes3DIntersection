using Cubes3DIntersection.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cubes3DIntersection.Application.Models
{
    public class Points3DModel : IModelBase
    {
        [JsonIgnore]
        public virtual int Id { get; set; }

        [Required]
        public IEnumerable<Point3DModel> PointCoordinates { get; set; }

        //public Points3DModel()
        //{
        //}

        //public Points3DModel(IEnumerable<Point3DModel> pointCoordinates)
        //{
        //    PointCoordinates = pointCoordinates ?? throw new ArgumentException(nameof(pointCoordinates));
        //}
    }
}