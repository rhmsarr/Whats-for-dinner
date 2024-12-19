using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsForDinner.Migrations
{
    /// <inheritdoc />
    public partial class addedingname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IngredientName",
                table: "RecipesIngredients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientName",
                table: "RecipesIngredients");
        }
    }
}
