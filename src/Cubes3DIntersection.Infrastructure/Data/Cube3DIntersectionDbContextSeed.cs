using Cubes3DIntersection.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubes3DIntersection.Infrastructure.Data
{
    public class Cube3DIntersectionDbContextSeed
    {
        public static async Task SeedAsync(Cube3DIntersectionDbContext Cube3DIntersectionDbContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                Cube3DIntersectionDbContext.Database.Migrate();
                Cube3DIntersectionDbContext.Database.EnsureCreated();

                if (!Cube3DIntersectionDbContext.Cube3DIntersection.Any())
                {
                    Cube3DIntersectionDbContext.Cube3DIntersection.AddRange(GetPreconfiguredCubes3DIntersection());
                    await Cube3DIntersectionDbContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<Cube3DIntersectionDbContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(Cube3DIntersectionDbContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Cube3DIntersection> GetPreconfiguredCubes3DIntersection()
        {
            return new List<Cube3DIntersection>()
            {
                new Cube3DIntersection()
                {
                        FirstCube3D = new Cube3D
                        {
                            PointCoordinates = Point3D.Create(1, 1, 2, 2, 2)
                        },
                        SecondCube3D = new Cube3D
                        {
                            PointCoordinates = Point3D.Create(2, 2, 4, 2, 2)
                        },
                        EdgesLength = 2
                },
                new Cube3DIntersection()
                {
                        FirstCube3D = new Cube3D
                        {
                            PointCoordinates = Point3D.Create(3, 3, 2, 2, 2)
                        },
                        SecondCube3D = new Cube3D
                        {
                            PointCoordinates = Point3D.Create(4, 4, 3, 2, 2)
                        },
                        EdgesLength = 2
                },
                new Cube3DIntersection()
                {
                        FirstCube3D = new Cube3D
                        {
                            PointCoordinates = Point3D.Create(5, 5, 2, 2, 2)
                        },
                        SecondCube3D = new Cube3D
                        {
                            PointCoordinates = Point3D.Create(6, 6, 10, 10, 10)
                        },
                        EdgesLength = 2
                }
            };
        }
    }
}
