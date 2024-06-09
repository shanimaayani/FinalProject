using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class Model2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_OrderId",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "CartItem",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_OrderId",
                table: "CartItem",
                newName: "IX_CartItem_CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_CartId",
                table: "CartItem",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_CartId",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartItem",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                newName: "IX_CartItem_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_OrderId",
                table: "CartItem",
                column: "OrderId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
