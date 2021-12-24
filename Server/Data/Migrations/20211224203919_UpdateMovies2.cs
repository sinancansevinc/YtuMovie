using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Data.Migrations
{
    public partial class UpdateMovies2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    adult = table.Column<bool>(type: "INTEGER", nullable: false),
                    backdrop_path = table.Column<string>(type: "TEXT", nullable: false),
                    genre_ids = table.Column<string>(type: "TEXT", nullable: false),
                    new_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    original_language = table.Column<string>(type: "TEXT", nullable: false),
                    original_title = table.Column<string>(type: "TEXT", nullable: false),
                    overview = table.Column<string>(type: "TEXT", nullable: false),
                    popularity = table.Column<double>(type: "REAL", nullable: false),
                    poster_path = table.Column<string>(type: "TEXT", nullable: false),
                    release_date = table.Column<string>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    trailer_link = table.Column<string>(type: "TEXT", nullable: false),
                    video = table.Column<bool>(type: "INTEGER", nullable: false),
                    vote_average = table.Column<double>(type: "REAL", nullable: false),
                    vote_count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.id);
                });
        }
    }
}
