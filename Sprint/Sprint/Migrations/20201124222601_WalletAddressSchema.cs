using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class WalletAddressSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "Wallet",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Month",
                table: "Wallet",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Wallet",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a2712888-33cb-4137-b089-894b7326ad6f");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b04ac107-c2a0-4297-bbaf-f0976ce5e06e");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9f26980d-8098-46ab-843b-b2d8ff903979", "9dd7f504-86d8-4a4c-aa88-2535ad947084", "AQAAAAEAACcQAAAAELU+szr20ZJu3sIyE60CXxvFiRLd1davtXF8p+Ytc4zMP+C+lInWpszo75yJLizFsA==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "32af3121-7c5a-48f3-8816-a8db8973eacb", "ed37b2e4-340e-41f0-864d-03f2fd03b013", "AQAAAAEAACcQAAAAEBAKiTiLjYw0QXpwEX1AEzqIsI+4GqvahY02oO0KSAMD+VDdKHDh9L1QfSRYUHwoxA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Wallet",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Month",
                table: "Wallet",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CardNumber",
                table: "Wallet",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b5c8d46b-0a75-4bfe-b493-050dd60dff3a");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "51ea9607-6bf2-4763-9719-bcc9402c15d3");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5397e2b9-6aea-4ceb-b082-fbf807d146a3", "1c2eaec1-ff73-4087-808a-59e309dcdc4d", "AQAAAAEAACcQAAAAEMKcB8ltCmaon7E/jkBQmgjRSPlNvXb/nHini58q3pFZnzhZNTjopsoPdEMWZRSNgw==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5f718acc-df8a-42e2-ab94-32533698f6af", "cfea4406-8bb9-431f-8072-4fa96a5e3298", "AQAAAAEAACcQAAAAEJQIsUYfjyIMopLL+U+7WtU2Fo20U70JbAJD/zADGQSyVYsowlgUHo+Bw7NpuzbDlA==" });
        }
    }
}
