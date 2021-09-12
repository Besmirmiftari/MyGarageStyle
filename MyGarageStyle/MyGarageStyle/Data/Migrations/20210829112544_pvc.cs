using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyGarageStyle.Data.Migrations
{
    public partial class pvc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PVCs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Dimensions = table.Column<string>(nullable: true),
                    Thickness = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true),
                    Guarantee = table.Column<string>(nullable: true),
                    Anticipated_Service_Life = table.Column<string>(nullable: true),
                    Material = table.Column<string>(nullable: true),
                    Joint = table.Column<string>(nullable: true),
                    InStock = table.Column<bool>(nullable: false),
                    Price = table.Column<string>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DefaultImage = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PVCs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PVCImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    PVCId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PVCImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PVCImages_PVCs_PVCId",
                        column: x => x.PVCId,
                        principalTable: "PVCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PVCImages_PVCId",
                table: "PVCImages",
                column: "PVCId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PVCImages");

            migrationBuilder.DropTable(
                name: "PVCs");
        }
    }
}
