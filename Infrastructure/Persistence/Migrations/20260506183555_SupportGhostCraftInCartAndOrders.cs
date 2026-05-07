using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SupportGhostCraftInCartAndOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId_ProductId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "GhostCraftOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "GhostCraftOrderId",
                table: "OrderItems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "GhostCraftOrders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "GhostCraftOrderId",
                table: "CartItems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GhostCraftOrderId",
                table: "OrderItems",
                column: "GhostCraftOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId_GhostCraftOrderId",
                table: "OrderItems",
                columns: new[] { "OrderId", "GhostCraftOrderId" },
                unique: true,
                filter: "\"GhostCraftOrderId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId_ProductId",
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId" },
                unique: true,
                filter: "\"ProductId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_GhostCraftOrderId",
                table: "CartItems",
                column: "GhostCraftOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_GhostCraftOrders_GhostCraftOrderId",
                table: "CartItems",
                column: "GhostCraftOrderId",
                principalTable: "GhostCraftOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_GhostCraftOrders_GhostCraftOrderId",
                table: "OrderItems",
                column: "GhostCraftOrderId",
                principalTable: "GhostCraftOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_GhostCraftOrders_GhostCraftOrderId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_GhostCraftOrders_GhostCraftOrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_GhostCraftOrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId_GhostCraftOrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId_ProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_GhostCraftOrderId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "GhostCraftOrderId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "GhostCraftOrders");

            migrationBuilder.DropColumn(
                name: "GhostCraftOrderId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "GhostCraftOrders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId_ProductId",
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);
        }
    }
}
