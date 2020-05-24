using Cubes3DIntersection.Application.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cubes3DIntersection.Application.Models
{
    public class Point3DModel : IModelBase
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public double X0 { get; set; }

        [Required]
        public double Y0 { get; set; }

        [Required]
        public double Z0 { get; set; }

        [JsonIgnore]
        public int Cube3DId { get; set; }

        [JsonIgnore]
        public Cube3DModel Cube3D { get; set; }

        public Point3DModel()
        {
        }

        public Point3DModel(double x0, double y0, double z0)
        {
            X0 = x0;
            Y0 = y0;
            Z0 = z0;
        }
    }
}
