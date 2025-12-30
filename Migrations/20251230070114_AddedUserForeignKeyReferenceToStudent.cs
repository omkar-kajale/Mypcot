using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mypcot.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserForeignKeyReferenceToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CreatedBy",
                table: "Students",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_CreatedBy",
                table: "Students",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_CreatedBy",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CreatedBy",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
