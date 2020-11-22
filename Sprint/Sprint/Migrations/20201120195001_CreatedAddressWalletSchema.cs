using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class CreatedAddressWalletSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    MailingStreet = table.Column<string>(nullable: false),
                    MailingCity = table.Column<string>(nullable: false),
                    MailingProvince = table.Column<string>(nullable: false),
                    MailingPostal = table.Column<string>(nullable: false),
                    ShippingStreet = table.Column<string>(nullable: false),
                    ShippingCity = table.Column<string>(nullable: false),
                    ShippingProvince = table.Column<string>(nullable: false),
                    ShippingPostal = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    WalletId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    CardNumber = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_Wallet_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 1,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 2,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 3,
                column: "RegularPrice",
                value: 11.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 4,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 5,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 6,
                column: "RegularPrice",
                value: 10.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 7,
                column: "RegularPrice",
                value: 17.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 8,
                column: "RegularPrice",
                value: 43.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 9,
                column: "RegularPrice",
                value: 26.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 10,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 11,
                column: "RegularPrice",
                value: 14.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 12,
                column: "RegularPrice",
                value: 26.95m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 14,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 15,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 16,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 17,
                column: "RegularPrice",
                value: 35.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 18,
                column: "RegularPrice",
                value: 69.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 19,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 20,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 23,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 24,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 25,
                column: "RegularPrice",
                value: 22.79m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 26,
                column: "RegularPrice",
                value: 5.69m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 27,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 28,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 29,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 30,
                column: "RegularPrice",
                value: 39.99m);

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

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 1,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 2,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 3,
                column: "RegularPrice",
                value: 11.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 4,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 5,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 6,
                column: "RegularPrice",
                value: 10.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 7,
                column: "RegularPrice",
                value: 17.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 8,
                column: "RegularPrice",
                value: 43.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 9,
                column: "RegularPrice",
                value: 26.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 10,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 11,
                column: "RegularPrice",
                value: 14.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 12,
                column: "RegularPrice",
                value: 26.95m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 14,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 15,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 16,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 17,
                column: "RegularPrice",
                value: 35.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 18,
                column: "RegularPrice",
                value: 69.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 19,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 20,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 23,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 24,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 25,
                column: "RegularPrice",
                value: 22.79m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 26,
                column: "RegularPrice",
                value: 5.69m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 27,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 28,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 29,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 30,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "db58e7b1-100a-4eeb-909f-a9ddba8992ee");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4b58576f-8b3f-4be6-89ab-0a850de055c6");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8a1b37c6-d79b-47df-b501-0bab422dd9ae", "ee4f65b8-2ad9-4db1-b365-947103accb1c", "AQAAAAEAACcQAAAAEMoG6XTCcfl4dtPp7DvuSu9NFLTq4WeY3akG4aatacd93gwwmv8NCSxmyA4m2iqj0A==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ef9a4c31-0a2a-481e-94ff-accaba908851", "939a14f3-912c-410d-9a30-7a3c9210de09", "AQAAAAEAACcQAAAAEOrljZq+qIcjZt8yJKi5cGTJ8Ddfu7ot0YP5eyTf0LtKuZJfHkpd67tASfmidyrX5g==" });
        }
    }
}
