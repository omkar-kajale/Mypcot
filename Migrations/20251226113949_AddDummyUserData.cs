using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mypcot.Migrations
{
    /// <inheritdoc />
    public partial class AddDummyUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: ["Id", "Login", "Password"],
                values: new object[,]
                {
                    { 1, "admin", "password" },
                    { 2, "user", "password" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValues: [1, 2]
            );
        }
    }
}
