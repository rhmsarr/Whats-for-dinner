using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsForDinner.Migrations
{
    /// <inheritdoc />
    public partial class modifiedrecipeIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RecipesIngredients_IngredientId",
                table: "RecipesIngredients",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesIngredients_Ingredients_IngredientId",
                table: "RecipesIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipesIngredients_Ingredients_IngredientId",
                table: "RecipesIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipesIngredients_IngredientId",
                table: "RecipesIngredients");
        }
    }
}
