using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTeamTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Teams_TeamId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadedFiles_Teams_TeamId",
                table: "LoadedFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Teams_TeamId",
                table: "Results");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_LoadedFiles_TeamId",
                table: "LoadedFiles");

            migrationBuilder.DropIndex(
                name: "IX_Answers_TeamId",
                table: "Answers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    KeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoadedFiles_TeamId",
                table: "LoadedFiles",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TeamId",
                table: "Answers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_KeycloakId",
                table: "Teams",
                column: "KeycloakId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Teams_TeamId",
                table: "Answers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadedFiles_Teams_TeamId",
                table: "LoadedFiles",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Teams_TeamId",
                table: "Results",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
