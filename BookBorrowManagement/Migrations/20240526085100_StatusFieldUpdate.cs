using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBorrowManagement.Migrations
{
    /// <inheritdoc />
    public partial class StatusFieldUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "User_Book_Management",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "User_Book_Management");
        }
    }
}
