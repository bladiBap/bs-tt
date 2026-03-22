using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolTestBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_products_currency_id",
                table: "products",
                column: "currency_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_currencies_currency_id",
                table: "products",
                column: "currency_id",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_currencies_currency_id",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_currency_id",
                table: "products");
        }
    }
}
