using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Cinemas_CinemaId1",
                table: "Halls");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Halls_HallId1",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_HallId1",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Halls_CinemaId1",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "HallId1",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "CinemaId1",
                table: "Halls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HallId1",
                table: "Seats",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaId1",
                table: "Halls",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_HallId1",
                table: "Seats",
                column: "HallId1");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_CinemaId1",
                table: "Halls",
                column: "CinemaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Cinemas_CinemaId1",
                table: "Halls",
                column: "CinemaId1",
                principalTable: "Cinemas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Halls_HallId1",
                table: "Seats",
                column: "HallId1",
                principalTable: "Halls",
                principalColumn: "Id");
        }
    }
}
