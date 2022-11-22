using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class WalkDifficultiesTableNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WalksDifficulty_WalkDifficultyId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalksDifficulty",
                table: "WalksDifficulty");

            migrationBuilder.RenameTable(
                name: "WalksDifficulty",
                newName: "WalkDifficulties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalkDifficulties",
                table: "WalkDifficulties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WalkDifficulties_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId",
                principalTable: "WalkDifficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WalkDifficulties_WalkDifficultyId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalkDifficulties",
                table: "WalkDifficulties");

            migrationBuilder.RenameTable(
                name: "WalkDifficulties",
                newName: "WalksDifficulty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalksDifficulty",
                table: "WalksDifficulty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WalksDifficulty_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId",
                principalTable: "WalksDifficulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
