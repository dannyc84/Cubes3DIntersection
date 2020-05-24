using Microsoft.EntityFrameworkCore.Migrations;

namespace Cubes3DIntersection.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Points3D",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X0 = table.Column<double>(nullable: false),
                    Y0 = table.Column<double>(nullable: false),
                    Z0 = table.Column<double>(nullable: false),
                    Cube3DId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points3D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cube3D",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cube3D", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cube3D_Points3D_Id",
                        column: x => x.Id,
                        principalTable: "Points3D",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cube3DIntersection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    SecondCube3DId = table.Column<int>(nullable: false),
                    EdgesLength = table.Column<double>(nullable: false),
                    Collision = table.Column<bool>(nullable: true),
                    IntersectionVolume = table.Column<double>(nullable: true),
                    Cube3DId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cube3DIntersection", x => new { x.Id, x.SecondCube3DId });
                    table.ForeignKey(
                        name: "FK_Cube3DIntersection_Cube3D_Cube3DId",
                        column: x => x.Cube3DId,
                        principalTable: "Cube3D",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cube3DIntersection_Cube3D_Id",
                        column: x => x.Id,
                        principalTable: "Cube3D",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cube3DIntersection_Cube3D_SecondCube3DId",
                        column: x => x.SecondCube3DId,
                        principalTable: "Cube3D",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Edges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point3DCenter = table.Column<double>(nullable: false),
                    Start = table.Column<double>(nullable: false),
                    End = table.Column<double>(nullable: false),
                    EdgesLength = table.Column<double>(nullable: false),
                    Cube3DId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Edges_Cube3D_Cube3DId",
                        column: x => x.Cube3DId,
                        principalTable: "Cube3D",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cube3DIntersection_Cube3DId",
                table: "Cube3DIntersection",
                column: "Cube3DId");

            migrationBuilder.CreateIndex(
                name: "IX_Cube3DIntersection_SecondCube3DId",
                table: "Cube3DIntersection",
                column: "SecondCube3DId");

            migrationBuilder.CreateIndex(
                name: "IX_Edges_Cube3DId",
                table: "Edges",
                column: "Cube3DId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cube3DIntersection");

            migrationBuilder.DropTable(
                name: "Edges");

            migrationBuilder.DropTable(
                name: "Cube3D");

            migrationBuilder.DropTable(
                name: "Points3D");
        }
    }
}
