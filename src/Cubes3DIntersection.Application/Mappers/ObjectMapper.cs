using AutoMapper;
using System;

namespace Cubes3DIntersection.Application.Mappers
{
    // The best implementation of AutoMapper for class libraries - https://stackoverflow.com/questions/26458731/how-to-configure-auto-mapper-in-class-library-project
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<Cubes3DIntersectionDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
