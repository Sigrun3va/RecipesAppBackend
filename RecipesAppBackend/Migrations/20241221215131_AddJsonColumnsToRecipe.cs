using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddJsonColumnsToRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "Recipes",
                newName: "IngredientsJson");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Recipes",
                newName: "CategoryJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IngredientsJson",
                table: "Recipes",
                newName: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "CategoryJson",
                table: "Recipes",
                newName: "Category");
        }
    }
}
