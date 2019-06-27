using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieBlend.Data.Migrations
{
    public partial class Discussion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscussionData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Poster_ID = table.Column<string>(nullable: false),
                    Movie_ID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Movie_name = table.Column<string>(nullable: true),
                    poster_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscussionData");
        }
    }
}
