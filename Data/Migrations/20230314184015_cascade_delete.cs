using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class cascade_delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Downloads_Games",
                table: "Downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_Downloads_Users",
                table: "Downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesDevelopers_Developers",
                table: "GamesDevelopers");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesDevelopers_Games",
                table: "GamesDevelopers");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Games",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Downloads_Games",
                table: "Downloads",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Downloads_Users",
                table: "Downloads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesDevelopers_Developers",
                table: "GamesDevelopers",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "DeveloperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesDevelopers_Games",
                table: "GamesDevelopers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Games",
                table: "Likes",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Downloads_Games",
                table: "Downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_Downloads_Users",
                table: "Downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesDevelopers_Developers",
                table: "GamesDevelopers");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesDevelopers_Games",
                table: "GamesDevelopers");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Games",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Downloads_Games",
                table: "Downloads",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Downloads_Users",
                table: "Downloads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesDevelopers_Developers",
                table: "GamesDevelopers",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesDevelopers_Games",
                table: "GamesDevelopers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Games",
                table: "Likes",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
