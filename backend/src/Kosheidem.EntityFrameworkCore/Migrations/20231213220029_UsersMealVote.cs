using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kosheidem.Migrations
{
    /// <inheritdoc />
    public partial class UsersMealVote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "AbpUsers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MealVoteUser",
                columns: table => new
                {
                    MealVotesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealVoteUser", x => new { x.MealVotesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_MealVoteUser_AbpUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealVoteUser_MealVotes_MealVotesId",
                        column: x => x.MealVotesId,
                        principalTable: "MealVotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealVoteUser_UsersId",
                table: "MealVoteUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealVoteUser");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "AbpUsers");
        }
    }
}
