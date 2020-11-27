using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class UpdatedAddressSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailingCity",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "MailingPostal",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "MailingProvince",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "MailingStreet",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ShippingCity",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ShippingPostal",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ShippingProvince",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ShippingStreet",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Address",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postal",
                table: "Address",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Address",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Address",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Postal",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "MailingCity",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MailingPostal",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MailingProvince",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MailingStreet",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingCity",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingPostal",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingProvince",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingStreet",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c79f7820-bd06-44f1-a044-366878682cfb");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a9a8e91a-f76e-4e43-a784-ad2686a65447");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a582711c-8cd8-4970-b69a-e0c5f6218103", "8178451b-5fa6-42c7-a9ea-c6c1b0389726", "AQAAAAEAACcQAAAAEIRrP6ebxAl/7ZSqLf6DBDN8s3p9R1rkYs6404DQ/JIdfrzZ1IPYB1M5dRKGNJr9aQ==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "90a2d81f-b7a3-40ff-8b9f-ab008abcda0d", "0c9bb182-a340-4e46-a44d-321ffcd9bbcf", "AQAAAAEAACcQAAAAEEPg9Mf+0w6zcdjJFNekapP2yBkamQzJoE5A1ED+3x3HbLdPeh8CXCk/drEj3mW3OA==" });
        }
    }
}
