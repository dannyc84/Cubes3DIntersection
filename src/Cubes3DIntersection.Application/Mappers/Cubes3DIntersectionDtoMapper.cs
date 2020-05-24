using AutoMapper;
using Cubes3DIntersection.Application.Models;
using Cubes3DIntersection.Core.Entities;

namespace Cubes3DIntersection.Application.Mappers
{
    public class Cubes3DIntersectionDtoMapper : Profile
    {
        public Cubes3DIntersectionDtoMapper()
        {
            CreateMap<Cube3DIntersection, Cube3DIntersectionModel>().ReverseMap();
            CreateMap<Cube3D, Cube3DModel>().ReverseMap();
            CreateMap<Point3D, Point3DModel>().ReverseMap();
            CreateMap<Edge, EdgeModel>().ReverseMap();
        }
    }
}
