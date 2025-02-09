using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRental.Migrations
{
    /// <inheritdoc />
    public partial class AddHouseDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Houses",
                newName: "Nabolagsinfo");

            migrationBuilder.AddColumn<string>(
                name: "Beskrivelse",
                table: "Houses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fasiliteter",
                table: "Houses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HouseImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HouseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_HouseImages_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseImages_HouseId",
                table: "HouseImages",
                column: "HouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseImages");

            migrationBuilder.DropColumn(
                name: "Beskrivelse",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Fasiliteter",
                table: "Houses");

            migrationBuilder.RenameColumn(
                name: "Nabolagsinfo",
                table: "Houses",
                newName: "ImageUrl");
        }
    }
}
