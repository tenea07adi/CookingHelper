using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddedLoginAttemptsCounterToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginAttemptsStreak",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginAttemptsStreak",
                table: "Users");
        }
    }
}
