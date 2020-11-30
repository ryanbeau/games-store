using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class UpdateOderSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "OrderItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemNumber",
                table: "OrderItem",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OrderItemsAmount",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderShippingHandlingAmount",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderTaxAmount",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Postal",
                table: "Address",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "69d314f2-0487-4fd2-9d29-c56d312e86c4");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "62db92ec-80b6-4156-8449-04e1887aadad");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "10c5e423-8504-4733-9352-5cefa1149c3b", "06949549-6299-4b55-949a-efa73669ab64", "AQAAAAEAACcQAAAAEG9cf9iOYOWWpExw1g9byVEx6ibclUut+nINnntyQ3/wuVgoJja/0zd4IbkLUNmQPA==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f6f86f7-4fb4-484e-84a9-a8496e9a6921", "f2a948a2-3b7f-499f-9f4a-0f5c863e2b4c", "AQAAAAEAACcQAAAAEBSl1ScYA/h+7fPIeXnelXu03nuT3mVirNrfdhEK652JjitLj42Mw59slfGtLmu+Bg==" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_GameId",
                table: "OrderItem",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Game",
                table: "OrderItem",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Game",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_GameId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ItemNumber",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderItemsAmount",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderShippingHandlingAmount",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderTaxAmount",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Postal",
                table: "Address",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "58d1c40a-90b4-47b1-9f00-753e2cd44458");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8f0b356a-83d0-4160-b258-26f0849249d2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "54331bd3-78bf-470f-b1a7-13b9079f0e3a", "7207bc29-df76-4da9-b18f-5d835603eab1", "AQAAAAEAACcQAAAAEMgQJTBX5qXc0YlaEYl4wVsZrDGxfm/7UU1as7smMUlyz0SadwQWTFlh78oM9k+Lhg==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "96ecd5bd-c2a9-498c-b9dd-d6f67c673beb", "59d6e6aa-83e8-42fd-a287-1b7025098000", "AQAAAAEAACcQAAAAEJmy00MyfmzFoo5RAhq1X5LI/IsP7S0JEhJoDrRjmFQRerB7vEhIptiTEMbs8QRKgA==" });
        }
    }
}
