using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeArticles.Data.Migrations
{
    public partial class ChangingKeyLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesLikes_Users_UserIdUser",
                table: "ArticlesLikes");

            migrationBuilder.DropIndex(
                name: "IX_ArticlesLikes_UserIdUser",
                table: "ArticlesLikes");

            migrationBuilder.DropColumn(
                name: "UserIdUser",
                table: "ArticlesLikes");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesLikes_IdUser",
                table: "ArticlesLikes",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesLikes_Users_IdUser",
                table: "ArticlesLikes",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesLikes_Users_IdUser",
                table: "ArticlesLikes");

            migrationBuilder.DropIndex(
                name: "IX_ArticlesLikes_IdUser",
                table: "ArticlesLikes");

            migrationBuilder.AddColumn<Guid>(
                name: "UserIdUser",
                table: "ArticlesLikes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesLikes_UserIdUser",
                table: "ArticlesLikes",
                column: "UserIdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesLikes_Users_UserIdUser",
                table: "ArticlesLikes",
                column: "UserIdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
