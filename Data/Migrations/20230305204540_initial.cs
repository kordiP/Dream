using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    developer_id = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.developer_id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    age_requirements = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    balance = table.Column<decimal>(type: "decimal(12,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    required_memory = table.Column<double>(type: "float", nullable: false),
                    genre_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.game_id);
                    table.ForeignKey(
                        name: "FK_Games_Genres",
                        column: x => x.genre_id,
                        principalTable: "Genres",
                        principalColumn: "genre_id");
                });

            migrationBuilder.CreateTable(
                name: "Downloads",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    game_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Downloads", x => new { x.user_id, x.game_id });
                    table.ForeignKey(
                        name: "FK_Downloads_Games",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "game_id");
                    table.ForeignKey(
                        name: "FK_Downloads_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Games_Developers",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "int", nullable: false),
                    developer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games_Developers", x => new { x.game_id, x.developer_id });
                    table.ForeignKey(
                        name: "FK_Games_Developers_Developers",
                        column: x => x.developer_id,
                        principalTable: "Developers",
                        principalColumn: "developer_id");
                    table.ForeignKey(
                        name: "FK_Games_Developers_Games",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "game_id");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    game_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.user_id, x.game_id });
                    table.ForeignKey(
                        name: "FK_Likes_Games",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "game_id");
                    table.ForeignKey(
                        name: "FK_Likes_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "UK_Email_Developer",
                table: "Developers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_game_id",
                table: "Downloads",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_genre_id",
                table: "Games",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Developers_developer_id",
                table: "Games_Developers",
                column: "developer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_game_id",
                table: "Likes",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "UK_Email_User",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Username_User",
                table: "Users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Downloads");

            migrationBuilder.DropTable(
                name: "Games_Developers");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
