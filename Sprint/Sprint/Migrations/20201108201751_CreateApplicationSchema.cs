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
                    Developer = table.Column<string>(unicode: false, maxLength: 48, nullable: false),
                    RegularPrice = table.Column<decimal>(nullable: false, defaultValue: 0m)
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
                name: "UserRelationship",
                columns: table => new
                {
                    UserRelationshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatingUser = table.Column<int>(nullable: false),
                    RelatedUser = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRelationship", x => x.UserRelationshipId);
                    table.ForeignKey(
                        name: "FK_User_Related",
                        column: x => x.RelatedUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Relating",
                        column: x => x.RelatingUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Discount",
                columns: table => new
                {
                    DiscountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    DiscountPrice = table.Column<decimal>(nullable: false),
                    DiscountStart = table.Column<DateTime>(nullable: false),
                    DiscountFinish = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_Game_Discount",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
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
                    { 1, "b852f83a-ac5d-4c3b-89ef-70e60c6f864b", "Admin", "ADMIN" },
                    { 2, "416ac185-ceef-4d72-9656-01f52fa2083c", "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "AccountNum", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "7d93dc65-40da-4b95-b0a6-8785be7efd20", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1f0b52f5-9fdc-4d96-9062-b31e2bd381d0", "admin@admin.com", true, "Other", false, null, "Admin", "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAELD/iRjSZGy7qSRxUj5R0y08sqY+WiVMY473Ef502bgnGK+WYMBBb37WjZiUBONofw==", "555-555-5555", false, "", false, "admin" },
                    { 2, 0, "f75d8b70-ce0f-42c5-9c05-3f996b263b34", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0b792e51-b6fe-43cc-9835-8e94468178f5", "user@user.com", true, "Other", false, null, "User", "USER@USER.COM", "USER", "AQAAAAEAACcQAAAAEEcfez/McSJqZmI/rmoYmwnHRuYcuDvDW+qTvCgAdnyAk2FFYqECz/OOT3JGbgFHdw==", "555-555-5555", false, "", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "Developer", "GameTypeId", "Name", "RegularPrice" },
                values: new object[] { 4, "2K Games", 1, "Borderlands 3", 79.99m });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "Developer", "GameTypeId", "Name" },
                values: new object[] { 13, "Roblox Corporation", 9, "Roblox" });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "Developer", "GameTypeId", "Name", "RegularPrice" },
                values: new object[] { 12, "Mojang", 9, "Minecraft", 26.95m });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "Developer", "GameTypeId", "Name" },
                values: new object[,]
                {
                    { 22, "Zenimax Online Studios", 8, "The Elder Scrolls® Online" },
                    { 21, "Bungie", 8, "Destiny 2" }
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "Developer", "GameTypeId", "Name", "RegularPrice" },
                values: new object[,]
                {
                    { 24, "EA Black Box", 7, "Need for Speed: Carbon", 19.99m },
                    { 23, "Milestone", 7, "WRC 3: FIA World Rally Championship", 39.99m },
                    { 2, "EA Sports", 7, "Madden NFL 20", 29.99m },
                    { 28, "Firaxis Games", 6, "Sid Meier's Civilization VI", 79.99m },
                    { 25, "Haemimont Games", 6, "Tropico 5", 22.79m },
                    { 26, "Innersloth", 5, "Among Us", 5.69m },
                    { 8, "Squad", 5, "Kerbal Space Program", 43.99m },
                    { 7, "Spiderling Studios", 5, "Besiege", 17.49m },
                    { 1, "SCS Software", 5, "Euro Truck Simulator 2", 21.99m },
                    { 30, "Bethesda", 4, "Fallout 4", 39.99m },
                    { 9, "Electronic Arts", 4, "Dragon Age: Origins", 26.99m },
                    { 6, "Bethesda", 4, "Fallout: New Vegas", 10.99m },
                    { 3, "Klei Entertainment", 3, "Don't Starve", 11.49m },
                    { 29, "Guerrilla", 2, "Horizon Zero Dawn", 59.99m },
                    { 27, "Rockstar Games", 2, "Red Dead Redemption II", 79.99m },
                    { 19, "Nintendo", 2, "The Legend of Zelda: Breath of the Wild", 79.99m },
                    { 16, "Monomi Park", 2, "Slime Rancher", 21.99m },
                    { 15, "Rockstar Games", 2, "Grand Theft Auto V", 29.99m },
                    { 11, "Ubisoft", 2, "Far Cry 3 - Blood Dragon", 14.99m },
                    { 10, "Ubisoft", 2, "Far Cry 3", 39.99m },
                    { 5, "Crystal Dynamics", 2, "Tomb Raider", 19.99m },
                    { 20, "Activision", 1, "Call of Duty: Black Ops 4", 79.99m },
                    { 18, "NetherRealm Studios", 1, "Mortal Kombat 11", 69.99m },
                    { 17, "Psyonix", 1, "Rocket League", 35.99m },
                    { 14, "Nintendo", 1, "Super Mario Odyssey", 59.99m }
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
                table: "Discount",
                columns: new[] { "DiscountId", "DiscountFinish", "DiscountPrice", "DiscountStart", "GameId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 8, 23, 0, 0, 0, DateTimeKind.Unspecified), 19.99m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 30 },
                    { 4, new DateTime(2020, 12, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 11.99m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 6, new DateTime(2020, 12, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 11.59m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 3, new DateTime(2020, 12, 8, 23, 0, 0, 0, DateTimeKind.Unspecified), 59.99m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 2, new DateTime(2020, 12, 8, 23, 0, 0, 0, DateTimeKind.Unspecified), 2.69m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 26 },
                    { 5, new DateTime(2020, 12, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 12.49m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 16 }
                });

            migrationBuilder.InsertData(
                table: "GameImage",
                columns: new[] { "GameImageId", "GameId", "ImageType", "ImageURL" },
                values: new object[,]
                {
                    { 1, 1, 1, "https://cdn.steamgriddb.com/grid/4c22bd444899d3b6047a10b20a2f26db.png" },
                    { 7, 7, 1, "https://cdn.steamgriddb.com/grid/a3171cc0f610fdfdf460831fb25a3dc7.png" },
                    { 8, 8, 1, "https://cdn.steamgriddb.com/grid/5e26566dffe850373e9a5121703034a1.png" },
                    { 26, 26, 1, "https://cdn.steamgriddb.com/grid/75c0aa52af187c4cd20744efafa1c7c7.png" },
                    { 25, 25, 1, "https://cdn.steamgriddb.com/grid/c967fb654df41177901d1f5f135bf9e6.png" },
                    { 28, 28, 1, "https://cdn.steamgriddb.com/grid/c9ee6a825655d889ae6a84bde2802bc2.png" },
                    { 2, 2, 1, "https://cdn.steamgriddb.com/grid/097e232de59f809f5a1cdf88e1240b08.png" },
                    { 23, 23, 1, "https://cdn.steamgriddb.com/grid/4a8520defd77a137222438d72ed7afd2.png" },
                    { 24, 24, 1, "https://cdn.steamgriddb.com/grid/ee955e252af3c85e66e15864e31174fe.png" },
                    { 21, 21, 1, "https://cdn.steamgriddb.com/grid/f5e083092550d2f93898e9829e677e39.png" },
                    { 22, 22, 1, "https://cdn.steamgriddb.com/grid/56c12a4512e84416de450db11ab040c3.png" },
                    { 30, 30, 1, "https://cdn.steamgriddb.com/grid/60c60a4ffa03bde6f8c83533d465ef5c.png" },
                    { 4, 4, 1, "https://cdn.steamgriddb.com/grid/7e04e496f1cf3896708f48127a7b65de.png" },
                    { 6, 6, 1, "https://cdn.steamgriddb.com/grid/5248e5118c84beea359b6ea385393661.png" },
                    { 12, 12, 1, "https://cdn.steamgriddb.com/grid/6db2fc0f9848c8830f2c5ad73e78ea75.png" },
                    { 3, 3, 1, "https://cdn.steamgriddb.com/grid/b16a06a5ea94028944a81ad5bbdbb8ca.png" },
                    { 29, 29, 1, "https://cdn.steamgriddb.com/grid/5ec5d5702a083583b268f32dde14b419.png" },
                    { 27, 27, 1, "https://cdn.steamgriddb.com/grid/41b28a11da13a0384a9b75f95244e8e8.png" },
                    { 19, 19, 1, "https://cdn.steamgriddb.com/grid/2da535ad78bb2e93aa448b1a4a61134e.png" },
                    { 16, 16, 1, "https://cdn.steamgriddb.com/grid/5f65c233d57a4b31b1e4edbaa79bf6ca.png" },
                    { 15, 15, 1, "https://cdn.steamgriddb.com/grid/3aa4cb2017fe681acd92bbea6b9f6015.png" },
                    { 11, 11, 1, "https://cdn.steamgriddb.com/grid/1051f72ed869290c51ee34a72b1d01df.png" },
                    { 10, 10, 1, "https://cdn.steamgriddb.com/grid/84b3f2becbf70a03239d7fae55dcaa40.png" },
                    { 5, 5, 1, "https://cdn.steamgriddb.com/grid/6be910cdb73c47cb973a944c03f5c7b1.png" },
                    { 20, 20, 1, "https://cdn.steamgriddb.com/grid/cb0fb5b71dd8266417731afb0e7a0864.png" },
                    { 18, 18, 1, "https://cdn.steamgriddb.com/grid/884738b4332ababd678ca505f4e04f4d.png" },
                    { 17, 17, 1, "https://cdn.steamgriddb.com/grid/ea3a48c74a9efb9a08635fe7990347cc.png" },
                    { 14, 14, 1, "https://cdn.steamgriddb.com/grid/5549f6da5ec3b191b672e682e4735d71.png" },
                    { 9, 9, 1, "https://cdn.steamgriddb.com/grid/c1537c9ed39baee3476c6fdd666b5fd8.png" },
                    { 13, 13, 1, "https://cdn.steamgriddb.com/grid/2a574bcb25a0ae1faad7c630370e6234.png" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discount_GameId",
                table: "Discount",
                column: "GameId");

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
                name: "IX_UserRelationship_RelatedUser",
                table: "UserRelationship",
                column: "RelatedUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelationship_RelatingUser_RelatedUser",
                table: "UserRelationship",
                columns: new[] { "RelatingUser", "RelatedUser" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discount");

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
                name: "UserRelationship");

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
