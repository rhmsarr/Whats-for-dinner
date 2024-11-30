using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsForDinner.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Categories_CategoryId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Ingredients",
                newName: "IngredientCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_CategoryId",
                table: "Ingredients",
                newName: "IX_Ingredients_IngredientCategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "IngredientCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Categories_IngredientCategoryId",
                table: "Ingredients",
                column: "IngredientCategoryId",
                principalTable: "Categories",
                principalColumn: "IngredientCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Categories_IngredientCategoryId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "IngredientCategoryId",
                table: "Ingredients",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_IngredientCategoryId",
                table: "Ingredients",
                newName: "IX_Ingredients_CategoryId");

            migrationBuilder.RenameColumn(
                name: "IngredientCategoryId",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Categories_CategoryId",
                table: "Ingredients",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
