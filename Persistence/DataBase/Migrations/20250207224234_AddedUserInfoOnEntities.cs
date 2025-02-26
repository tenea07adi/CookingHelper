using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserInfoOnEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "UserInvites",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "UserInvites",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "RecipeIngredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "RecipeIngredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PreparationSteps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "PreparationSteps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Ingredients",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserInvites");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserInvites");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PreparationSteps");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PreparationSteps");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Ingredients");
        }
    }
}
