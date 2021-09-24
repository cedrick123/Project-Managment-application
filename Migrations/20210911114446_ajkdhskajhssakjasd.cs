using Microsoft.EntityFrameworkCore.Migrations;

namespace new_project.Migrations
{
    public partial class ajkdhskajhssakjasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "projectId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_projectId",
                table: "AspNetUsers",
                column: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_projects_projectId",
                table: "AspNetUsers",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_projects_projectId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_projectId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "projectId",
                table: "AspNetUsers");
        }
    }
}
