using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class UpdateUserCreatePlatformTypeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredGameTypeId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreferredPlatformId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReceivePromotionalEmails",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PlatformType",
                columns: table => new
                {
                    PlatformTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformType", x => x.PlatformTypeId);
                });

            migrationBuilder.InsertData(
                table: "PlatformType",
                columns: new[] { "PlatformTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Other" },
                    { 19, "PC Other" },
                    { 18, "PC Linux" },
                    { 17, "PC Mac" },
                    { 16, "PC Windows" },
                    { 15, "Switch" },
                    { 14, "Wii U" },
                    { 13, "Wii" },
                    { 11, "Nintendo DS" },
                    { 12, "Nintendo 3DS" },
                    { 9, "PlayStation 4" },
                    { 8, "PlayStation 3" },
                    { 7, "PlayStation 2" },
                    { 6, "PlayStation" },
                    { 5, "Xbox Series" },
                    { 4, "Xbox One" },
                    { 3, "Xbox 360" },
                    { 2, "Xbox" },
                    { 10, "PlayStation 5" }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6bfc27d7-3144-4fa6-ac2f-abe1187fdbb6");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5ca599fc-7204-4c82-aa5b-0af7578cd952");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ffc227a9-96b0-4951-ad6c-5bc1f948e7e2", "7e6ed11b-5a22-412c-b60e-65912c9fb448", "AQAAAAEAACcQAAAAEHVY3pK3DWMAjEnOo+K7laiYX7Y9Cj0qvq15yZBC/G5q+jTkh/WyJ6/u8v0AZ2JrMQ==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aeb93db8-3e1f-4674-97e0-c3143867528e", "209b608d-6083-4ad4-b634-84d512e9ef08", "AQAAAAEAACcQAAAAEGuwl2+JPgCvyvlkn8ivx3HoNayGtvAuUXTAFQPZ5cfZP1qWFRqf0etB9+/sXh5oGw==" });

            migrationBuilder.CreateIndex(
                name: "IX_User_PreferredGameTypeId",
                table: "User",
                column: "PreferredGameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PreferredPlatformId",
                table: "User",
                column: "PreferredPlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_GameType",
                table: "User",
                column: "PreferredGameTypeId",
                principalTable: "GameType",
                principalColumn: "GameTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_PlatformType",
                table: "User",
                column: "PreferredPlatformId",
                principalTable: "PlatformType",
                principalColumn: "PlatformTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_GameType",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_PlatformType",
                table: "User");

            migrationBuilder.DropTable(
                name: "PlatformType");

            migrationBuilder.DropIndex(
                name: "IX_User_PreferredGameTypeId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PreferredPlatformId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PreferredGameTypeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PreferredPlatformId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ReceivePromotionalEmails",
                table: "User");

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
