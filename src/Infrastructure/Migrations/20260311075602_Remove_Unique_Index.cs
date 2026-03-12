using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Unique_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoadedFiles_TeamId_CreatedDate",
                table: "LoadedFiles");

            migrationBuilder.DropIndex(
                name: "IX_Answers_TeamId_CreatedDate",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_LoadedFiles_TeamId",
                table: "LoadedFiles",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TeamId",
                table: "Answers",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoadedFiles_TeamId",
                table: "LoadedFiles");

            migrationBuilder.DropIndex(
                name: "IX_Answers_TeamId",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_LoadedFiles_TeamId_CreatedDate",
                table: "LoadedFiles",
                columns: new[] { "TeamId", "CreatedDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TeamId_CreatedDate",
                table: "Answers",
                columns: new[] { "TeamId", "CreatedDate" },
                unique: true);
        }
    }
}
