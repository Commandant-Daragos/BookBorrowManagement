using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBorrowManagement.Migrations
{
    /// <inheritdoc />
    public partial class Delete_status_field_from_User_Book_Management_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "User_Book_Management");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "User_Book_Management",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
