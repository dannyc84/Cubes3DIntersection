﻿// <auto-generated />
using System;
using Cubes3DIntersection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cubes3DIntersection.Infrastructure.Migrations
{
    [DbContext(typeof(Cube3DIntersectionDbContext))]
    partial class Cube3DIntersectionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cubes3DIntersection.Core.Entities.Cube3D", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cube3D");
                });

            modelBuilder.Entity("Cubes3DIntersection.Core.Entities.Cube3DIntersection", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("SecondCube3DId")
                        .HasColumnType("int");

                    b.Property<bool?>("Collision")
                        .HasColumnType("bit");

                    b.Property<int?>("Cube3DId")
                        .HasColumnType("int");

                    b.Property<double>("EdgesLength")
                        .HasColumnType("float");

                    b.Property<double?>("IntersectionVolume")
                        .HasColumnType("float");

                    b.HasKey("Id", "SecondCube3DId");

                    b.HasIndex("Cube3DId");

                    b.HasIndex("SecondCube3DId");

                    b.ToTable("Cube3DIntersection");
                });

            modelBuilder.Entity("Cubes3DIntersection.Core.Entities.Edge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cube3DId")
                        .HasColumnType("int");

                    b.Property<double>("EdgesLength")
                        .HasColumnType("float");

                    b.Property<double>("End")
                        .HasColumnType("float");

                    b.Property<double>("Point3DCenter")
                        .HasColumnType("float");

                    b.Property<double>("Start")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Cube3DId");

                    b.ToTable("Edges");
                });

            modelBuilder.Entity("Cubes3DIntersection.Core.Entities.Point3D", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cube3DId")
                        .HasColumnType("int");

                    b.Property<double>("X0")
                        .HasColumnType("float");

                    b.Property<double>("Y0")
                        .HasColumnType("float");

                    b.Property<double>("Z0")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Points3D");
                });

            modelBuilder.Entity("Cubes3DIntersection.Core.Entities.Cube3D", b =>
                {
                    b.HasOne("Cubes3DIntersection.Core.Entities.Point3D", "PointCoordinates")
                        .WithOne("Cube3D")
                        .HasForeignKey("Cubes3DIntersection.Core.Entities.Cube3D", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cubes3DIntersection.Core.Entities.Cube3DIntersection", b =>
                {
                    b.HasOne("Cubes3DIntersection.Core.Entities.Cube3D", null)
                        .WithMany("Cube3DIntersection")
                        .HasForeignKey("Cube3DId");

                    b.HasOne("Cubes3DIntersection.Core.Entities.Cube3D", "FirstCube3D")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Cubes3DIntersection.Core.Entities.Cube3D", "SecondCube3D")
                        .WithMany()
                        .HasForeignKey("SecondCube3DId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Cubes3DIntersection.Core.Entities.Edge", b =>
                {
                    b.HasOne("Cubes3DIntersection.Core.Entities.Cube3D", null)
                        .WithMany("Edges")
                        .HasForeignKey("Cube3DId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
