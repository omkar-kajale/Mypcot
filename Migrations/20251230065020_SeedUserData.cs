using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mypcot.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: ["Id", "Login", "Password", "Name"],
                values: new object[,]
                {
                    { 1, "admin", "password", "omkar" },
                    { 2, "user", "password", "test" }
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
