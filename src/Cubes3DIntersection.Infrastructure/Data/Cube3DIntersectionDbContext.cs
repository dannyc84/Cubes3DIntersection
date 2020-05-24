using Cubes3DIntersection.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cubes3DIntersection.Infrastructure.Data
{
    public class Cube3DIntersectionDbContext : DbContext
    {
        public DbSet<Cube3DIntersection> Cube3DIntersection { get; set; }

        public DbSet<Cube3D> Cube3D { get; set; }

        public DbSet<Point3D> Points3D { get; set; }

        public DbSet<Edge> Edges { get; set; }

        public Cube3DIntersectionDbContext(DbContextOptions<Cube3DIntersectionDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<Cube3DIntersection>()
               .HasKey(ci => new { ci.Id, ci.SecondCube3DId });

            modelBuilder.Entity<Cube3DIntersection>()
                .HasOne(ci => ci.FirstCube3D)
                .WithMany()
                .HasForeignKey(ci => ci.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cube3DIntersection>()
                .HasOne(ci => ci.SecondCube3D)
                .WithMany()
                .HasForeignKey(ci => ci.SecondCube3DId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Point3D>()
                .HasOne(c => c.Cube3D)
                .WithOne(c => c.PointCoordinates)
                .HasForeignKey<Cube3D>(c => c.Id);
        }
    }
}
