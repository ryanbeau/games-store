using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class CreateApplicationSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameType",
                columns: table => new
                {
                    GameTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameType", x => x.GameTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AccountNum = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 128, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    WishlistVisibility = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 48, nullable: false),
                    Developer = table.Column<string>(unicode: false, maxLength: 48, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Game_GameType",
                        column: x => x.GameTypeId,
                        principalTable: "GameType",
                        principalColumn: "GameTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.UserId, x.ProviderKey, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_UserLogins_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameImage",
                columns: table => new
                {
                    GameImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    ImageURL = table.Column<string>(nullable: false),
                    ImageType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameImage", x => x.GameImageId);
                    table.ForeignKey(
                        name: "FK_GameImage_Game",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    ReviewContent = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Game",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGameWishlist",
                columns: table => new
                {
                    UserGameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameWishlist", x => x.UserGameId);
                    table.ForeignKey(
                        name: "FK_Wishlist_Game",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wishlist_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "GameType",
                columns: new[] { "GameTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Action-Adventure" },
                    { 3, "Adventure" },
                    { 4, "Role-Playing" },
                    { 5, "Simulation" },
                    { 6, "Strategy" },
                    { 7, "Sports" },
                    { 8, "MMO" },
                    { 9, "Sandbox" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "cf8c3185-1df4-4468-a2a1-a35f9596bc03", "Admin", "ADMIN" },
                    { 2, "ec500148-c625-4f41-8867-5541236e566a", "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "AccountNum", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "d633cbc9-d529-4208-9360-dd7eb4d6c645", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "097ebb49-bef0-46b0-a475-c521bf4a3480", "admin@admin.com", true, "Other", false, null, "Admin", "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEFD+nyIJBNIO0gIBqIczHb4VGDjjjIj9v/fSBD717Oo9WXA5HbQjTN1yZ+Q4fKoK1A==", "555-555-5555", false, "", false, "admin" },
                    { 2, 0, "ff3b2187-a7f6-4e00-bcd8-5ee364794e0c", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4453025a-28c5-4563-a599-2110b6b0960f", "user@user.com", true, "Other", false, null, "User", "USER@USER.COM", "USER", "AQAAAAEAACcQAAAAEEeKTLO5perZOvCf1nist6BGtLeja+jbEAA48Dmxrn7mzRZrnzS/h4wwwSyl7SZuVw==", "555-555-5555", false, "", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "Developer", "GameTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Activision", 1, "Call of Duty: Modern Warfare" },
                    { 13, "Roblox Corporation", 9, "Roblox" },
                    { 12, "Mojang", 9, "Minecraft" },
                    { 11, "Epic Games", 9, "Fortnite" },
                    { 18, "Riot Games", 8, "League of Legends" },
                    { 3, "Electronic Arts", 7, "Madden NFL 20" },
                    { 2, "2K Sports", 7, "NBA 2K20" },
                    { 8, "Square Enix", 4, "Kingdom Hearts III" },
                    { 19, "Nintendo", 2, "The Legend of Zelda: Breath of the Wild" },
                    { 20, "Activision", 1, "Call of Duty: Black Ops 4" },
                    { 17, "Psyonix", 1, "Rocket League" },
                    { 16, "Sony Interactive Entertainment", 1, "Marvel’s Spider-Man" },
                    { 15, "Rockstar Games", 1, "Grand Theft Auto V" },
                    { 14, "Nintendo", 1, "Super Mario Odyssey" },
                    { 10, "Nintendo", 1, "Mario Kart 8 Deluxe" },
                    { 9, "Ubisoft", 1, "Tom Clancy's The Division 2" },
                    { 7, "Nintendo", 1, "Super Smash Bros" },
                    { 6, "Electronic Arts", 1, "Star Wars Jedi: Fallen Order" },
                    { 5, "Warner Bros. Interactive Entertainment", 1, "Mortal Kombat II" },
                    { 4, "2K Games", 1, "Borderlands 3" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "GameImage",
                columns: new[] { "GameImageId", "GameId", "ImageType", "ImageURL" },
                values: new object[,]
                {
                    { 1, 1, 1, "https://cdn.steamgriddb.com/grid/d5c91983451d0fa52a7ce530a3714ab7.png" },
                    { 4, 4, 1, "https://cdn.steamgriddb.com/grid/7e04e496f1cf3896708f48127a7b65de.png" },
                    { 5, 5, 1, "https://cdn.steamgriddb.com/grid/9a3bd37a71b632e7726f149bbd771052.png" },
                    { 7, 7, 1, "https://cdn.steamgriddb.com/grid/71e9c6620d381d60196ebe694840aaaa.png" },
                    { 9, 9, 1, "https://cdn.steamgriddb.com/grid/29ddbdb402491a6aa97964a8139a1356.png" },
                    { 10, 10, 1, "https://cdn.steamgriddb.com/grid/c6d4eb15f1e84a36eff58eca3627c82e.png" },
                    { 14, 14, 1, "https://cdn.steamgriddb.com/grid/5549f6da5ec3b191b672e682e4735d71.png" },
                    { 17, 17, 1, "https://cdn.steamgriddb.com/grid/ea3a48c74a9efb9a08635fe7990347cc.png" },
                    { 20, 20, 1, "https://cdn.steamgriddb.com/grid/cb0fb5b71dd8266417731afb0e7a0864.png" },
                    { 19, 19, 1, "https://cdn.steamgriddb.com/grid/2da535ad78bb2e93aa448b1a4a61134e.png" },
                    { 2, 2, 1, "https://cdn.steamgriddb.com/grid/767b2cc82cecc0385fe6f1086dd2c748.png" },
                    { 3, 3, 1, "https://cdn.steamgriddb.com/grid/94cd0468d6f321ec192c9e301ba30e85.png" },
                    { 18, 18, 1, "https://cdn.steamgriddb.com/grid/f4a331b7a22d1b237565d8813a34d8ac.png" },
                    { 12, 12, 1, "https://cdn.steamgriddb.com/grid/6db2fc0f9848c8830f2c5ad73e78ea75.png" },
                    { 13, 13, 1, "https://cdn.steamgriddb.com/grid/2a574bcb25a0ae1faad7c630370e6234.png" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameTypeId",
                table: "Game",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GameImage_GameId",
                table: "GameImage",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_GameId",
                table: "Review",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId_GameId",
                table: "Review",
                columns: new[] { "UserId", "GameId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameWishlist_GameId",
                table: "UserGameWishlist",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameWishlist_UserId_GameId",
                table: "UserGameWishlist",
                columns: new[] { "UserId", "GameId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameImage");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserGameWishlist");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "GameType");
        }
    }
}
