using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRental.Migrations
{
    /// <inheritdoc />
    public partial class MakeEndDateNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaseAgreements_Owners_OwnerId",
                table: "LeaseAgreements");

            

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "LeaseAgreements");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "LeaseAgreements",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "LeaseAgreements",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "LeaseAgreements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LeaseAgreements_OwnerId",
                table: "LeaseAgreements",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseAgreements_Owners_OwnerId",
                table: "LeaseAgreements",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
