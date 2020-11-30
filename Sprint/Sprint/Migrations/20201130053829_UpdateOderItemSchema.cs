using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class UpdateOderItemSchema : Migration
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
                value: "daa270df-251d-470f-8b1a-0efbc93696f1");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f5a52565-5d0f-4d61-9549-fba8df5aab08");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d9606fa-ecbc-40bd-8426-b67dadd12949", "68660dad-b267-4d1b-947a-98a8612c02af", "AQAAAAEAACcQAAAAEFGZbjeoUx6X0B5OYKCVZk4ht/dp8QWR8jJD0DgqMXzDO+53X1sMlLkMBT+TUyzKtg==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "442077de-dd35-4563-bc6c-821743909f78", "23289860-b69a-488d-be22-23f1027f486b", "AQAAAAEAACcQAAAAEPSKowZ0uoGl/RH1LA5cWtlfY5DjwRbZYDSQAR2CEShOrPLoIeT5oIIaB4I2XZcXfQ==" });

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
