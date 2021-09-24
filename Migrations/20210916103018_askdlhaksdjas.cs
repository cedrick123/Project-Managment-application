using Microsoft.EntityFrameworkCore.Migrations;

namespace new_project.Migrations
{
    public partial class askdlhaksdjas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "roleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_roleId",
                table: "AspNetUsers",
                column: "roleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_roleId",
                table: "AspNetUsers",
                column: "roleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_roleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_roleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "roleId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
