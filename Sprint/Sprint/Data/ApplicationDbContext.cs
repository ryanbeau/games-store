using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sprint.Enums;
using Sprint.Models;
using System;

namespace Sprint.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public virtual DbSet<Wallet> Wallet { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<GameDiscount> GameDiscounts { get; set; }
        public virtual DbSet<GameImage> GameImages { get; set; }
        public virtual DbSet<PlatformType> PlatformTypes { get; set; }
        public virtual DbSet<GameType> GameTypes { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<CartGame> CartGames { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<UserGameWishlist> UserGameWishlists { get; set; }
        public virtual DbSet<UserRelationship> UserRelationships { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventUser> EventUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MsSqlLocalDb;Database=RDSCGameStore;Trusted_Connection=True;");
            }
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = 2, Name = "Member", NormalizedName = "MEMBER" }
            );

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
                new User 
                {
                    Id = 1,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Password1!"),
                    SecurityStamp = string.Empty,
                    AccountNum = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    Gender = "Other",
                    PhoneNumber = "555-555-5555",
                    BirthDate = new DateTime(1970, 01, 01)
                },
                new User
                {
                    Id = 2,
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@user.com",
                    NormalizedEmail = "USER@USER.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Password1!"),
                    SecurityStamp = string.Empty,
                    AccountNum = Guid.NewGuid().ToString(),
                    Name = "User",
                    Gender = "Other",
                    PhoneNumber = "555-555-5555",
                    BirthDate = new DateTime(1970, 01, 01)
                });

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 },
                new IdentityUserRole<int> { RoleId = 2, UserId = 2 }
            );

            modelBuilder.Entity<GameType>().HasData(
                new GameType { GameTypeId = 1, Name = "Action" }, // 3
                new GameType { GameTypeId = 2, Name = "Action-Adventure" }, // 6
                new GameType { GameTypeId = 3, Name = "Adventure" }, // 1
                new GameType { GameTypeId = 4, Name = "Role-Playing" }, // 2
                new GameType { GameTypeId = 5, Name = "Simulation" }, // 3
                new GameType { GameTypeId = 6, Name = "Strategy" }, // -
                new GameType { GameTypeId = 7, Name = "Sports" }, // 2
                new GameType { GameTypeId = 8, Name = "MMO" }, // 2
                new GameType { GameTypeId = 9, Name = "Sandbox" } // 2
            );

            modelBuilder.Entity<Game>().HasData(
                new Game { GameId = 1, Name = "Euro Truck Simulator 2", Developer = "SCS Software", RegularPrice = 21.99m, GameTypeId = 5 }, // simulation
                new Game { GameId = 2, Name = "Madden NFL 20", Developer = "EA Sports", RegularPrice = 29.99m, GameTypeId = 7 }, // sports
                new Game { GameId = 3, Name = "Don't Starve", Developer = "Klei Entertainment", RegularPrice = 11.49m, GameTypeId = 3 }, // adventure
                new Game { GameId = 4, Name = "Borderlands 3", Developer = "2K Games", RegularPrice = 79.99m, GameTypeId = 1 }, // action
                new Game { GameId = 5, Name = "Tomb Raider", Developer = "Crystal Dynamics", RegularPrice = 19.99m, GameTypeId = 2 }, // action-adventure
                new Game { GameId = 6, Name = "Fallout: New Vegas", Developer = "Bethesda", RegularPrice = 10.99m, GameTypeId = 4 }, // role-playing
                new Game { GameId = 7, Name = "Besiege", Developer = "Spiderling Studios", RegularPrice = 17.49m, GameTypeId = 5 }, // simulation
                new Game { GameId = 8, Name = "Kerbal Space Program", Developer = "Squad", RegularPrice = 43.99m, GameTypeId = 5 }, // simulation
                new Game { GameId = 9, Name = "Dragon Age: Origins", Developer = "Electronic Arts", RegularPrice = 26.99m, GameTypeId = 4 }, // role-playing
                new Game { GameId = 10, Name = "Far Cry 3", Developer = "Ubisoft", RegularPrice = 39.99m, GameTypeId = 2 }, // action-adventure
                new Game { GameId = 11, Name = "Far Cry 3 - Blood Dragon", Developer = "Ubisoft", RegularPrice = 14.99m, GameTypeId = 2 }, // action-adventure
                new Game { GameId = 12, Name = "Minecraft", Developer = "Mojang", RegularPrice = 26.95m, GameTypeId = 9 }, // sandbox
                new Game { GameId = 13, Name = "Roblox", Developer = "Roblox Corporation", RegularPrice = 0.0m, GameTypeId = 9 }, // sandbox
                new Game { GameId = 14, Name = "Super Mario Odyssey", Developer = "Nintendo", RegularPrice = 59.99m, GameTypeId = 1 }, // action
                new Game { GameId = 15, Name = "Grand Theft Auto V", Developer = "Rockstar Games", RegularPrice = 29.99m, GameTypeId = 2 }, // action-adventure
                new Game { GameId = 16, Name = "Slime Rancher", Developer = "Monomi Park", RegularPrice = 21.99m, GameTypeId = 2 }, // action-adventure
                new Game { GameId = 17, Name = "Rocket League", Developer = "Psyonix", RegularPrice = 35.99m, GameTypeId = 1 }, // action
                new Game { GameId = 18, Name = "Mortal Kombat 11", Developer = "NetherRealm Studios", RegularPrice = 69.99m, GameTypeId = 1 }, // action
                new Game { GameId = 19, Name = "The Legend of Zelda: Breath of the Wild", RegularPrice = 79.99m, Developer = "Nintendo", GameTypeId = 2 }, // action-adventure
                new Game { GameId = 20, Name = "Call of Duty: Black Ops 4", Developer = "Activision", RegularPrice = 79.99m, GameTypeId = 1 }, // action
                new Game { GameId = 21, Name = "Destiny 2", Developer = "Bungie", RegularPrice = 0, GameTypeId = 8 }, // mmo
                new Game { GameId = 22, Name = "The Elder Scrolls® Online", Developer = "Zenimax Online Studios", RegularPrice = 0, GameTypeId = 8 }, // mmo
                new Game { GameId = 23, Name = "WRC 3: FIA World Rally Championship", Developer = "Milestone", RegularPrice = 39.99m, GameTypeId = 7 }, // sports
                new Game { GameId = 24, Name = "Need for Speed: Carbon", Developer = "EA Black Box", RegularPrice = 19.99m, GameTypeId = 7 }, // sports
                new Game { GameId = 25, Name = "Tropico 5", Developer = "Haemimont Games", RegularPrice = 22.79m, GameTypeId = 6 }, // strategy
                new Game { GameId = 26, Name = "Among Us", Developer = "Innersloth", RegularPrice = 5.69m, GameTypeId = 5 }, // simulation
                new Game { GameId = 27, Name = "Red Dead Redemption II", Developer = "Rockstar Games", RegularPrice = 79.99m, GameTypeId = 2 }, // action-adventure
                new Game { GameId = 28, Name = "Sid Meier's Civilization VI", Developer = "Firaxis Games", RegularPrice = 79.99m, GameTypeId = 6 }, // strategy
                new Game { GameId = 29, Name = "Horizon Zero Dawn", Developer = "Guerrilla", RegularPrice = 59.99m, GameTypeId = 2 }, // action-adventure
                new Game { GameId = 30, Name = "Fallout 4", Developer = "Bethesda", RegularPrice = 39.99m, GameTypeId = 4 } // role-playing
            );

            modelBuilder.Entity<GameDiscount>().HasData(
                // Fallout 4
                new GameDiscount { DiscountId = 1, GameId = 30, DiscountPrice = 19.99m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2020, 12, 08, 23, 0, 0) },
                // Among Us
                new GameDiscount { DiscountId = 2, GameId = 26, DiscountPrice = 2.69m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2020, 12, 08, 23, 0, 0) },
                // The Legend of Zelda: Breath of the Wild
                new GameDiscount { DiscountId = 3, GameId = 19, DiscountPrice = 59.99m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2020, 12, 08, 23, 0, 0) },
                // Besiege
                new GameDiscount { DiscountId = 4, GameId = 7, DiscountPrice = 11.99m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2020, 12, 25, 23, 0, 0) },
                // Slime Rancher
                new GameDiscount { DiscountId = 5, GameId = 16, DiscountPrice = 12.49m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2020, 12, 25, 23, 0, 0) },
                // Tomb Raider
                new GameDiscount { DiscountId = 6, GameId = 5, DiscountPrice = 11.59m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2020, 12, 25, 23, 0, 0) },
                // Red Dead Redemption II
                new GameDiscount { DiscountId = 7, GameId = 27, DiscountPrice = 59.99m, DiscountStart = new DateTime(2020, 11, 20), DiscountFinish = new DateTime(2021, 7, 24, 23, 0, 0) },
                // Tropico 5
                new GameDiscount { DiscountId = 8, GameId = 25, DiscountPrice = 13.69m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2021, 7, 24, 23, 0, 0) },
                // Mortal Kombat 11
                new GameDiscount { DiscountId = 9, GameId = 18, DiscountPrice = 48.99m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2021, 7, 24, 23, 0, 0) },
                // Grand Theft Auto V
                new GameDiscount { DiscountId = 10, GameId = 15, DiscountPrice = 14.99m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2021, 7, 24, 23, 0, 0) },
                // Borderlands 3
                new GameDiscount { DiscountId = 11, GameId = 4, DiscountPrice = 52.79m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2021, 7, 24, 23, 0, 0) },
                // Don't Starve
                new GameDiscount { DiscountId = 12, GameId = 3, DiscountPrice = 7.99m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2021, 7, 24, 23, 0, 0) }
            );

            modelBuilder.Entity<GameImage>().HasData(
                // Euro Truck Simulator 2
                new GameImage { GameImageId = 1, GameId = 1, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/4c22bd444899d3b6047a10b20a2f26db.png" },
                new GameImage { GameImageId = 31, GameId = 1, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_b70d5295ecc738dc2279797ec8351ac0fdf139f4.jpg" },
                new GameImage { GameImageId = 32, GameId = 1, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_2082e17ee1dad2004c48e689a005f8f684f5b645.jpg" },
                new GameImage { GameImageId = 33, GameId = 1, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_68aea6cce2261f8cdd52fe39bc1fceddc783ed69.jpg" },
                new GameImage { GameImageId = 34, GameId = 1, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_cf79030615e1188ed7e31f36c6b4dcf8b5cf4512.jpg" },
                new GameImage { GameImageId = 35, GameId = 1, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_ec04158ea75cfac3ea2703d00ab06cd8dfd4416a.jpg" },
                new GameImage { GameImageId = 36, GameId = 1, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_f30c62166abbbb87b1c4a822f46b538d84978816.jpg" },
                new GameImage { GameImageId = 37, GameId = 1, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_3424d0fd16eaefeeef2d3e601db52dc62dc73c63.jpg" },
                // Madden NFL 20
                new GameImage { GameImageId = 2, GameId = 2, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/097e232de59f809f5a1cdf88e1240b08.png" },
                new GameImage { GameImageId = 38, GameId = 2, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1239520/ss_02f624e7b929d29632416474c90fc9b046f9e9fc.jpg" },
                new GameImage { GameImageId = 39, GameId = 2, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1239520/ss_b38af388931fd2a652c5849a6d4ef25dbedf4645.jpg" },
                new GameImage { GameImageId = 40, GameId = 2, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1239520/ss_1478e30c80afd1ce72f6c6887691b76769130666.jpg" },
                new GameImage { GameImageId = 41, GameId = 2, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1239520/ss_f410761ffd30001b88a964356abad036a5ece574.jpg" },
                // Don't Starve
                new GameImage { GameImageId = 3, GameId = 3, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/b16a06a5ea94028944a81ad5bbdbb8ca.png" },
                new GameImage { GameImageId = 42, GameId = 3, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_09bb6bdce4fa085c2c4a9a8f48ea52d3051b44bc.jpg" },
                new GameImage { GameImageId = 43, GameId = 3, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_26b6414b4ae07cfc2e2d15bd6ff315a4678f00f3.jpg" },
                new GameImage { GameImageId = 44, GameId = 3, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_3d32a04e77363f3c8179a319de6f90ac1b8b2e0e.jpg" },
                new GameImage { GameImageId = 45, GameId = 3, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_0167fbdf9d30407734baf3ab3b08213945738166.jpg" },
                new GameImage { GameImageId = 46, GameId = 3, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_38d4d1b21050fc4b3978fcf65c909260d3673fb7.jpg" },
                new GameImage { GameImageId = 47, GameId = 3, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_1d3d9b7d9d752666feb9853215c118104816eee2.jpg" },
                // Borderlands 3
                new GameImage { GameImageId = 4, GameId = 4, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/7e04e496f1cf3896708f48127a7b65de.png" },
                new GameImage { GameImageId = 48, GameId = 4, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_9868ee40f39749a4c8222502cf86525ee32c1bef.jpg" },
                new GameImage { GameImageId = 49, GameId = 4, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_624638e46ed590d4bb1835558a5ab0981f7baadd.jpg" },
                new GameImage { GameImageId = 50, GameId = 4, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_3531d9f91265d94fc06f6587eba1ca49f2c423d1.jpg" },
                new GameImage { GameImageId = 51, GameId = 4, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_6f1836277ffe8733503a9446d51b8c7eb3d20d5f.jpg" },
                new GameImage { GameImageId = 52, GameId = 4, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_bd8b719de92cfc9e65cd96d5da74426918964291.jpg" },
                new GameImage { GameImageId = 53, GameId = 4, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_f983c0c1cc566b8ca21a6c45e6f044b57aff0f0f.jpg" },
                new GameImage { GameImageId = 54, GameId = 4, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_bb22ac18c1db1a87d779db0c3fb480eb1ce79f0e.jpg" },
                // Tomb Raider
                new GameImage { GameImageId = 5, GameId = 5, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/6be910cdb73c47cb973a944c03f5c7b1.png" },
                new GameImage { GameImageId = 55, GameId = 5, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_65861a8ea2efcb01fca8aa4b1233663bb053ab54.jpg" },
                new GameImage { GameImageId = 56, GameId = 5, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_c93f9a97f2fd890f21c829cf8781850484eec7f3.jpg" },
                new GameImage { GameImageId = 57, GameId = 5, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_846fd1166a03b1d3147618aaba1ff7ef4477085d.jpg" },
                new GameImage { GameImageId = 58, GameId = 5, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_8f34a87469f3a0c73049cbd0469bdff6e3d22713.jpg" },
                new GameImage { GameImageId = 59, GameId = 5, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_cd920308517efb19c11b44e251af89e40fb412d5.jpg" },
                new GameImage { GameImageId = 60, GameId = 5, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_b49a25cb879b6dbaa6f851eae3b5bf6d3fc04478.jpg" },
                // Fallout: New Vegas
                new GameImage { GameImageId = 6, GameId = 6, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5248e5118c84beea359b6ea385393661.png" },
                new GameImage { GameImageId = 61, GameId = 6, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/22490/ss_ec8a28942fcb5cb15718f949ab81124932a5084d.jpg" },
                new GameImage { GameImageId = 62, GameId = 6, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/22490/ss_ac7dbb6b5d1353ec1e66110fe652883b957a70e3.jpg" },
                new GameImage { GameImageId = 63, GameId = 6, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/22490/ss_909a95f11266fed10eba4282b36608a9e731a1c5.jpg" },
                new GameImage { GameImageId = 64, GameId = 6, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/22490/ss_b898b51f69d795b804374bb6396c7c24b23545d3.jpg" },
                // Besiege
                new GameImage { GameImageId = 7, GameId = 7, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/a3171cc0f610fdfdf460831fb25a3dc7.png" },
                new GameImage { GameImageId = 65, GameId = 7, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_922d27a19a98dd259e23b6f82901728da1e91bb8.jpg" },
                new GameImage { GameImageId = 66, GameId = 7, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_cbf706e7a6860429d148e49220dddf7ecac20cf7.jpg" },
                new GameImage { GameImageId = 67, GameId = 7, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_62f421579193a15026c439f6c1685a28017b84ba.jpg" },
                new GameImage { GameImageId = 68, GameId = 7, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_e7dc933b7e6a321f6be37367fce68a39dac26d16.jpg" },
                new GameImage { GameImageId = 69, GameId = 7, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_f280252d02632345eeba8877ece485c8582dd30d.jpg" },
                new GameImage { GameImageId = 70, GameId = 7, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_9fe75fccba08bfb18338f8833b030775a30b3685.jpg" },
                new GameImage { GameImageId = 71, GameId = 7, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_4c0020e484b09452631df8cfc42a97d9d1ddb0c1.jpg" },
                // Kerbal Space Program
                new GameImage { GameImageId = 8, GameId = 8, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5e26566dffe850373e9a5121703034a1.png" },
                new GameImage { GameImageId = 72, GameId = 8, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220200/ss_c5b19aad79ad37ba36708f22742c1bf81f9220ca.jpg" },
                new GameImage { GameImageId = 73, GameId = 8, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220200/ss_f258c8ebef5e2bf47a2feca529a2cd3f864cfbb0.jpg" },
                new GameImage { GameImageId = 74, GameId = 8, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220200/ss_f93547d532f3b0b19dbc23a1500fd313f3619e03.jpg" },
                new GameImage { GameImageId = 75, GameId = 8, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220200/ss_9fc2032db736a2a9919aba739566a8353c2cd3cb.jpg" },
                // Dragon Age: Origins
                new GameImage { GameImageId = 9, GameId = 9, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/c1537c9ed39baee3476c6fdd666b5fd8.png" },
                new GameImage { GameImageId = 76, GameId = 9, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/17450/ss_c79860e70c98e653453f4dc227df5820d9f841ca.jpg" },
                new GameImage { GameImageId = 77, GameId = 9, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/17450/ss_082575907197c03f4e27816f1fca1bd7d5c97848.jpg" },
                new GameImage { GameImageId = 78, GameId = 9, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/17450/ss_066491f3039cd9919a8b1d3538d33100fe87dfca.jpg" },
                new GameImage { GameImageId = 79, GameId = 9, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/17450/ss_7d9dc6d7c7362bca257e035e2fe161c68005b9f2.jpg" },
                // Far Cry 3
                new GameImage { GameImageId = 10, GameId = 10, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/84b3f2becbf70a03239d7fae55dcaa40.png" },
                new GameImage { GameImageId = 80, GameId = 10, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_a85a01aae90c8ca69a05d32a1196ffaf6d943bd7.jpg" },
                new GameImage { GameImageId = 81, GameId = 10, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_6e7911d3622f3a801aeea1c2f8418f0f22880bb0.jpg" },
                new GameImage { GameImageId = 82, GameId = 10, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_c5c810244ba6dd899de44121b37d87fad2621d4c.jpg" },
                new GameImage { GameImageId = 83, GameId = 10, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_842bfdeb44368babd55ad93af1cbbf560f9fb9a1.jpg" },
                new GameImage { GameImageId = 84, GameId = 10, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_eed79518c510b7b8ce6fce9d1c350bfcea530993.jpg" },
                new GameImage { GameImageId = 85, GameId = 10, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_83af36773eb0393144cfefa80ceb6baf5c608cf7.jpg" },
                new GameImage { GameImageId = 86, GameId = 10, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_0c08ce5d7f63fd8cee404426f6d083d93105a924.jpg" },
                // Far Cry 3 - Blood Dragon
                new GameImage { GameImageId = 11, GameId = 11, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/1051f72ed869290c51ee34a72b1d01df.png" },
                new GameImage { GameImageId = 87, GameId = 11, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/233270/ss_602b7c88ab725ccabfd8ad7c94fb536875c329ad.jpg" },
                new GameImage { GameImageId = 88, GameId = 11, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/233270/ss_3210874e1d6e20965cc9c76ac2d7e899ef2b0b9f.jpg" },
                new GameImage { GameImageId = 89, GameId = 11, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/233270/ss_4670702422e948a9666c29814ac6cfdb941c5a4a.jpg" },
                new GameImage { GameImageId = 90, GameId = 11, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/233270/ss_040958f3839e7d1c3ae50c76200d0891c5ca6883.jpg" },
                // Minecraft
                new GameImage { GameImageId = 12, GameId = 12, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/6db2fc0f9848c8830f2c5ad73e78ea75.png" },
                // Roblox
                new GameImage { GameImageId = 13, GameId = 13, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/2a574bcb25a0ae1faad7c630370e6234.png" },
                // Super Mario Odyssey
                new GameImage { GameImageId = 14, GameId = 14, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5549f6da5ec3b191b672e682e4735d71.png" },
                // Grand Theft Auto V
                new GameImage { GameImageId = 15, GameId = 15, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/3aa4cb2017fe681acd92bbea6b9f6015.png" },
                new GameImage { GameImageId = 91, GameId = 15, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/271590/ss_bb2ee3b9b48a60857873192cfff10546e01d4a86.jpg" },
                new GameImage { GameImageId = 92, GameId = 15, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/271590/ss_cd721eb1856f0dd3b820e4e998c3b5fe7e7c9b4e.jpg" },
                new GameImage { GameImageId = 93, GameId = 15, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/271590/ss_f64515607fd627aa9436be3b15fdcb9e1c89bb19.jpg" },
                new GameImage { GameImageId = 94, GameId = 15, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/271590/ss_95a1f04eb687eae71478c0c5ba644da57e10f215.jpg" },
                // Slime Rancher
                new GameImage { GameImageId = 16, GameId = 16, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5f65c233d57a4b31b1e4edbaa79bf6ca.png" },
                new GameImage { GameImageId = 95, GameId = 16, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_5339a74c4563d40a1d8a5638db2a9ed59c5b883b.jpg" },
                new GameImage { GameImageId = 96, GameId = 16, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_d923566424cee1c6d82ccde7336b02057d3409fc.jpg" },
                new GameImage { GameImageId = 97, GameId = 16, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_633faaacb74e2da5baa356e0f18f9f73e777c4d2.jpg" },
                new GameImage { GameImageId = 98, GameId = 16, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_e43a384a5d5ab9bcb943423c1e264a54ce840c43.jpg" },
                new GameImage { GameImageId = 99, GameId = 16, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_72a4d284ccc4535d23ba4e12752b8e5a3939b88e.jpg" },
                new GameImage { GameImageId = 100, GameId = 16, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_7dde72f87afe373d4624f49bf81575f8aa2a80fd.jpg" },
                new GameImage { GameImageId = 101, GameId = 16, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_2e80c1d9b6f0f6bfdbbff18b84baca2d1794cd7c.jpg" },
                // Rocket League
                new GameImage { GameImageId = 17, GameId = 17, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/ea3a48c74a9efb9a08635fe7990347cc.png" },
                new GameImage { GameImageId = 102, GameId = 17, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/252950/ss_0ccc388fb5712016814e66dfa712285d13529bbc.jpg" },
                new GameImage { GameImageId = 103, GameId = 17, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/252950/ss_868ba56dd81b7622bd9ba7f878fd143d0900bcfd.jpg" },
                new GameImage { GameImageId = 104, GameId = 17, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/252950/ss_ba4f48d62ce18649310c16d11b80c33003c39886.jpg" },
                new GameImage { GameImageId = 105, GameId = 17, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/252950/ss_e8d08712cfffe7ec54889f12588c2c150537294e.jpg" },
                // Mortal Kombat 11
                new GameImage { GameImageId = 18, GameId = 18, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/884738b4332ababd678ca505f4e04f4d.png" },
                new GameImage { GameImageId = 106, GameId = 18, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/976310/ss_8f3f4ef12a5cfae47d8e768778c329f17ee8b320.jpg" },
                new GameImage { GameImageId = 107, GameId = 18, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/976310/ss_25db57e715957f9673a69723867942f79b8357d9.jpg" },
                new GameImage { GameImageId = 108, GameId = 18, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/976310/ss_5c4f1ad866b43b0d2aa18400216eb4e6168357b4.jpg" },
                new GameImage { GameImageId = 109, GameId = 18, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/976310/ss_5b88f071939e32c790dd9e84890d9c197956a7be.jpg" },
                // The Legend of Zelda: Breath of the Wild
                new GameImage { GameImageId = 19, GameId = 19, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/2da535ad78bb2e93aa448b1a4a61134e.png" },
                // Call of Duty: Black Ops 4
                new GameImage { GameImageId = 20, GameId = 20, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/cb0fb5b71dd8266417731afb0e7a0864.png" },
                // Destiny 2
                new GameImage { GameImageId = 21, GameId = 21, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/f5e083092550d2f93898e9829e677e39.png" },
                new GameImage { GameImageId = 110, GameId = 21, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1085660/ss_7fcc82f468fcf8278c7ffa95cebf949bfc6845fc.jpg" },
                new GameImage { GameImageId = 111, GameId = 21, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1085660/ss_d923711c0eb833b1df8607fa3df4dcebbe470cf2.jpg" },
                new GameImage { GameImageId = 112, GameId = 21, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1085660/ss_c142f5078ace9f5e2eb2c80aa3bf768e156b4ee9.jpg" },
                new GameImage { GameImageId = 113, GameId = 21, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1085660/ss_a9642404e586be28f856e8f02d038828f691a5ba.jpg" },
                // The Elder Scrolls® Online
                new GameImage { GameImageId = 22, GameId = 22, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/56c12a4512e84416de450db11ab040c3.png" },
                new GameImage { GameImageId = 114, GameId = 22, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/306130/ss_c1cdae9879550709486774eed3a2760d18955349.jpg" },
                new GameImage { GameImageId = 115, GameId = 22, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/306130/ss_16688139333e39593af0eccc77342165eacae0d0.jpg" },
                new GameImage { GameImageId = 116, GameId = 22, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/306130/ss_da66e194128088e46a5ecad2af455ae2ebe84be0.jpg" },
                new GameImage { GameImageId = 117, GameId = 22, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/306130/ss_48be777780be9babf0ffb6c77766d5b0776adc1f.jpg" },
                // WRC 3: FIA World Rally Championship
                new GameImage { GameImageId = 23, GameId = 23, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/4a8520defd77a137222438d72ed7afd2.png" },
                new GameImage { GameImageId = 118, GameId = 23, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/256330/ss_57b07abe4e9a1165da440a4b798f4383e560a2f7.jpg" },
                new GameImage { GameImageId = 119, GameId = 23, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/256330/ss_dc30c74424a74426194c924a6326e8ab9d2e6dc4.jpg" },
                new GameImage { GameImageId = 120, GameId = 23, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/256330/ss_ff04f7cbf0ad72b618ab6e17f6af2a424cbf5eaa.jpg" },
                new GameImage { GameImageId = 121, GameId = 23, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/256330/ss_2ca3198a526321a83deac259ba02f8690f64db88.jpg" },
                // Need for Speed: Carbon
                new GameImage { GameImageId = 24, GameId = 24, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/ee955e252af3c85e66e15864e31174fe.png" },
                new GameImage { GameImageId = 122, GameId = 24, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1262540/ss_76eb69e48a6a0cc256603c2aa0844e5e6d5c8168.jpg" },
                new GameImage { GameImageId = 123, GameId = 24, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1262540/ss_e720137320c342a6664e195bc875613f338397ca.jpg" },
                new GameImage { GameImageId = 124, GameId = 24, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1262540/ss_700fb95c4cfc073b2e0640dc5049a0df1c2940f4.jpg" },
                new GameImage { GameImageId = 125, GameId = 24, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1262540/ss_7da80363f25c7d7ccc95e7bfdc5d8091e0cea77e.jpg" },
                // Tropico 5
                new GameImage { GameImageId = 25, GameId = 25, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/c967fb654df41177901d1f5f135bf9e6.png" },
                new GameImage { GameImageId = 126, GameId = 25, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/245620/ss_d254f02db9453c6931b0850a62341d8651329095.jpg" },
                new GameImage { GameImageId = 127, GameId = 25, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/245620/ss_b81a5412d6d1edb80b7f90899a63c42043f5abcb.jpg" },
                new GameImage { GameImageId = 128, GameId = 25, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/245620/ss_48bb8b8874175b55a0661d242eec349cdaf5bad0.jpg" },
                new GameImage { GameImageId = 129, GameId = 25, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/245620/ss_ca56bd7c7cf60337f169d29115aa3f761422f551.jpg" },
                // Among Us
                new GameImage { GameImageId = 26, GameId = 26, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/75c0aa52af187c4cd20744efafa1c7c7.png" },
                new GameImage { GameImageId = 130, GameId = 26, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/945360/ss_c80d2f3fab624b18d9531adc6957767a7fede100.jpg" },
                new GameImage { GameImageId = 131, GameId = 26, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/945360/ss_649e19ff657fa518d4c2b45bed7ffdc4264a4b3a.jpg" },
                new GameImage { GameImageId = 132, GameId = 26, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/945360/ss_7ccc894b8f95091f608fa012450549091cce2423.jpg" },
                new GameImage { GameImageId = 133, GameId = 26, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/945360/ss_943c487e302fa5fc303d59e45a03218e25a2a59c.jpg" },
                // Red Dead Redemption II
                new GameImage { GameImageId = 27, GameId = 27, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/41b28a11da13a0384a9b75f95244e8e8.png" },
                new GameImage { GameImageId = 134, GameId = 27, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_66b553f4c209476d3e4ce25fa4714002cc914c4f.jpg" },
                new GameImage { GameImageId = 135, GameId = 27, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_bac60bacbf5da8945103648c08d27d5e202444ca.jpg" },
                new GameImage { GameImageId = 136, GameId = 27, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_668dafe477743f8b50b818d5bbfcec669e9ba93e.jpg" },
                new GameImage { GameImageId = 137, GameId = 27, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_4ce07ae360b166f0f650e9a895a3b4b7bf15e34f.jpg" },
                new GameImage { GameImageId = 138, GameId = 27, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_d1a8f5a69155c3186c65d1da90491fcfd43663d9.jpg" },
                // Sid Meier's Civilization VI
                new GameImage { GameImageId = 28, GameId = 28, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/c9ee6a825655d889ae6a84bde2802bc2.png" },
                new GameImage { GameImageId = 139, GameId = 28, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/289070/ss_36c63ebeb006b246cb740fdafeb41bb20e3b330d.jpg" },
                new GameImage { GameImageId = 140, GameId = 28, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/289070/ss_cf53258cb8c4d283e52cf8dce3edf8656f83adc6.jpg" },
                new GameImage { GameImageId = 141, GameId = 28, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/289070/ss_f501156a69223131ee8b12452f3003698334e964.jpg" },
                new GameImage { GameImageId = 142, GameId = 28, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/289070/ss_2be9153a2633e671c283e2dbcec64e2e4543f66f.jpg" },
                // Horizon Zero Dawn
                new GameImage { GameImageId = 29, GameId = 29, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5ec5d5702a083583b268f32dde14b419.png" },
                new GameImage { GameImageId = 143, GameId = 29, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1151640/ss_d09106060fb7de8bf342c23df18b14debc8a15a3.jpg" },
                new GameImage { GameImageId = 144, GameId = 29, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1151640/ss_271f850eec3f96b22aa17be35b948268e0771c7f.jpg" },
                new GameImage { GameImageId = 145, GameId = 29, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1151640/ss_15f5759c441e4e5f51e1a8ee333e4ab9df9aa783.jpg" },
                new GameImage { GameImageId = 146, GameId = 29, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/1151640/ss_9db45aa04e8c8b5043b479f42ed36296bfc3a918.jpg" },
                // Fallout 4
                new GameImage { GameImageId = 30, GameId = 30, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/60c60a4ffa03bde6f8c83533d465ef5c.png" },
                new GameImage { GameImageId = 147, GameId = 30, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/377160/ss_f7861bd71e6c0c218d8ff69fb1c626aec0d187cf.jpg" },
                new GameImage { GameImageId = 148, GameId = 30, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/377160/ss_c310f858e6a7b02ffa21db984afb0dd1b24c1423.jpg" },
                new GameImage { GameImageId = 149, GameId = 30, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/377160/ss_6834be966451a9b0f12eb4f68bfb0853ea0b7267.jpg" },
                new GameImage { GameImageId = 150, GameId = 30, ImageType = ImageType.Regular, ImageURL = "https://cdn.cloudflare.steamstatic.com/steam/apps/377160/ss_cd83d21b82e4c4e9a6d76edc98a8c2b70b1b5e9d.jpg" }
            );

            modelBuilder.Entity<PlatformType>().HasData(
                new PlatformType { PlatformTypeId = 1, Name = "Other" },
                new PlatformType { PlatformTypeId = 2, Name = "Xbox" },
                new PlatformType { PlatformTypeId = 3, Name = "Xbox 360" },
                new PlatformType { PlatformTypeId = 4, Name = "Xbox One" },
                new PlatformType { PlatformTypeId = 5, Name = "Xbox Series" },
                new PlatformType { PlatformTypeId = 6, Name = "PlayStation" },
                new PlatformType { PlatformTypeId = 7, Name = "PlayStation 2" },
                new PlatformType { PlatformTypeId = 8, Name = "PlayStation 3" },
                new PlatformType { PlatformTypeId = 9, Name = "PlayStation 4" },
                new PlatformType { PlatformTypeId = 10, Name = "PlayStation 5" },
                new PlatformType { PlatformTypeId = 11, Name = "Nintendo DS" },
                new PlatformType { PlatformTypeId = 12, Name = "Nintendo 3DS" },
                new PlatformType { PlatformTypeId = 13, Name = "Wii" },
                new PlatformType { PlatformTypeId = 14, Name = "Wii U" },
                new PlatformType { PlatformTypeId = 15, Name = "Switch" },
                new PlatformType { PlatformTypeId = 16, Name = "PC Windows" },
                new PlatformType { PlatformTypeId = 17, Name = "PC Mac" },
                new PlatformType { PlatformTypeId = 18, Name = "PC Linux" },
                new PlatformType { PlatformTypeId = 19, Name = "PC Other" }
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderId").UseIdentityColumn();

                entity.Property(e => e.UserId).HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.OrderDate).HasColumnName("OrderDate")
                    .IsRequired();

                entity.Property(e => e.OrderNumber).HasColumnName("OrderNumber")
                    .IsRequired();

                entity.Property(e => e.ShippingAddressId).HasColumnName("ShippingAddressId");

                entity.Property(e => e.BillingAddressId).HasColumnName("BillingAddressId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");

                entity.HasOne(d => d.ShippingAddress)
                    .WithMany(p => p.ShippedOrders)
                    .HasForeignKey(d => d.ShippingAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_ShippedAddress");

                entity.HasOne(d => d.BillingAddress)
                    .WithMany(p => p.BilledOrders)
                    .HasForeignKey(d => d.BillingAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_BilledAddress");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemId").UseIdentityColumn();

                entity.Property(e => e.OrderId).HasColumnName("OrderId")
                    .IsRequired();

                entity.Property(e => e.OwnerUserId).HasColumnName("OwnerUserId")
                    .IsRequired();

                entity.Property(e => e.PhysicallyOwned).HasColumnName("PhysicallyOwned")
                    .HasDefaultValue(false)
                    .IsRequired();

                entity.HasOne(d => d.OwnerUser)
                    .WithMany(p => p.OwnedItems)
                    .HasForeignKey(d => d.OwnerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_OwnerUser");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Order");
            });

            modelBuilder.Entity<PlatformType>(entity =>
            {
                entity.ToTable("PlatformType");

                entity.Property(e => e.PlatformTypeId).HasColumnName("PlatformTypeId").UseIdentityColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GameImage>(entity =>
            {
                entity.HasKey(e => e.GameImageId);

                entity.ToTable("GameImage");

                entity.Property(p => p.GameImageId).HasColumnName("GameImageId").UseIdentityColumn();

                entity.Property(e => e.GameId).HasColumnName("GameId")
                    .IsRequired();

                entity.Property(e => e.ImageURL).HasColumnName("ImageURL")
                    .IsRequired();

                entity.Property(e => e.ImageType).HasColumnName("ImageType")
                    .IsRequired();

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameImages)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameImage_Game");
            });

            modelBuilder.Entity<GameType>(entity =>
            {
                entity.ToTable("GameType");

                entity.Property(e => e.GameTypeId).HasColumnName("GameTypeId").UseIdentityColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.ToTable("Game");

                entity.Property(e => e.GameId).HasColumnName("GameId").UseIdentityColumn();

                entity.Property(e => e.Developer)
                    .IsRequired()
                    .HasColumnName("Developer")
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.RegularPrice)
                    .IsRequired()
                    .HasColumnName("RegularPrice")
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);

                entity.Property(e => e.GameTypeId).HasColumnName("GameTypeId");

                entity.HasOne(d => d.GameType)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GameTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_GameType");
            });

            modelBuilder.Entity<GameDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.ToTable("Discount");

                entity.Property(p => p.DiscountId).HasColumnName("DiscountId").UseIdentityColumn();

                entity.Property(e => e.GameId).HasColumnName("GameId")
                    .IsRequired();

                entity.Property(e => e.DiscountPrice).HasColumnName("DiscountPrice")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.DiscountStart).HasColumnName("DiscountStart")
                    .IsRequired();

                entity.Property(e => e.DiscountFinish).HasColumnName("DiscountFinish")
                    .IsRequired();

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.GameId)
                    .HasPrincipalKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Discount");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.WalletId);
                entity.ToTable("Wallet");

                entity.Property(e => e.WalletId).HasColumnName("WalletId").UseIdentityColumn();

                entity.Property(e => e.UserId).HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.CardNumber).HasColumnName("CardNumber")
                    .IsRequired();

                entity.Property(e => e.Month).HasColumnName("Month")
                .IsRequired();

                entity.Property(e => e.Year).HasColumnName("Year")
                .IsRequired();

                entity.HasOne(d => d.User)
                .WithMany(d => d.Wallets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wallet_User");

            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(p => p.AddressId);
                entity.ToTable("Address");

                entity.Property(e => e.AddressId).HasColumnName("AddressId").UseIdentityColumn();

                entity.Property(e => e.UserId).HasColumnName("UserId")
                    .IsRequired();
                entity.Property(e => e.Street).HasColumnName("Street")
                    .IsRequired();
                entity.Property(e => e.City).HasColumnName("City")
                    .IsRequired();
                entity.Property(e => e.Province).HasColumnName("Province")
                    .IsRequired();
                entity.Property(e => e.Postal).HasColumnName("Postal")
                    .IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(d => d.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_User");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.ReviewId);

                entity.ToTable("Review");

                entity.Property(p => p.ReviewId).HasColumnName("ReviewId").UseIdentityColumn();

                entity.Property(e => e.UserId).HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.GameId).HasColumnName("GameId")
                    .IsRequired();

                entity.Property(e => e.Rating).HasColumnName("Rating")
                    .IsRequired();

                entity.HasIndex(b => new { b.UserId, b.GameId })
                    .IsUnique();

                entity.Property(e => e.ReviewContent)
                    .IsRequired()
                    .HasColumnName("ReviewContent")
                    .IsUnicode(false);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Game");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_User");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("Event");

                entity.Property(p => p.EventId).HasColumnName("EventId").UseIdentityColumn();

                entity.Property(e => e.UserId).HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("EventName");

                entity.Property(e => e.EventDescription)
                    .IsRequired()
                    .HasColumnName("EventDescription");

                entity.Property(e => e.EventDateTime)
                    .IsRequired()
                    .HasColumnName("EventDateTime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CreatedEvents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_Created_User");
            });

            modelBuilder.Entity<EventUser>(entity =>
            {
                entity.HasKey(e => e.EventUserId);

                entity.ToTable("EventUser");

                entity.Property(p => p.EventId).HasColumnName("EventId")
                    .IsRequired();

                entity.Property(e => e.UserId).HasColumnName("UserId")
                    .IsRequired();

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventUsers)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.JoinedEvents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Event");
            });

            modelBuilder.Entity<CartGame>(entity =>
            {
                entity.HasKey(e => e.CartGameId);

                entity.ToTable("CartGame");

                entity.Property(p => p.CartGameId)
                    .HasColumnName("CartGameId")
                    .UseIdentityColumn();

                entity.Property(e => e.CartUserId)
                    .HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.ReceivingUserId)
                    .HasColumnName("ReceivingUserId")
                    .IsRequired();

                entity.Property(e => e.GameId)
                    .HasColumnName("GameId")
                    .IsRequired();

                entity.Property(e => e.AddedOn)
                    .HasColumnName("AddedOn")
                    .IsRequired();

                entity.HasIndex(b => new { b.CartUserId, b.ReceivingUserId, b.GameId })
                    .IsUnique();

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartGame_Game");

                entity.HasOne(d => d.CartUser)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartGame_CartUser");

                entity.HasOne(d => d.ReceivingUser)
                    .WithMany(p => p.ReceivingCartItems)
                    .HasForeignKey(d => d.ReceivingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartGame_ReceivingUser");
            });

            modelBuilder.Entity<UserGameWishlist>(entity =>
            {
                entity.HasKey(e => e.UserGameId);

                entity.ToTable("UserGameWishlist");

                entity.Property(p => p.UserGameId).HasColumnName("UserGameId").UseIdentityColumn();

                entity.Property(e => e.UserId).HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.GameId).HasColumnName("GameId")
                    .IsRequired();

                entity.Property(e => e.AddedOn).HasColumnName("AddedOn")
                    .IsRequired();

                entity.HasIndex(b => new { b.UserId, b.GameId })
                    .IsUnique();

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wishlist_Game");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wishlist_User");
            });

            modelBuilder.Entity<UserRelationship>(entity =>
            {
                entity.HasKey(e => e.UserRelationshipId);

                entity.ToTable("UserRelationship");

                entity.Property(p => p.UserRelationshipId).HasColumnName("UserRelationshipId").UseIdentityColumn();

                entity.Property(e => e.RelatingUserId).HasColumnName("RelatingUser")
                    .IsRequired();

                entity.Property(e => e.RelatedUserId).HasColumnName("RelatedUser")
                    .IsRequired();

                entity.Property(e => e.Type).HasColumnName("Type")
                    .IsRequired()
                    .HasDefaultValue(Relationship.Pending);

                entity.HasIndex(b => new { b.RelatingUserId, b.RelatedUserId })
                    .IsUnique();

                entity.HasOne(d => d.RelatingUser)
                    .WithMany(p => p.RelatingRelationships)
                    .HasForeignKey(d => d.RelatingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Relating");

                entity.HasOne(d => d.RelatedUser)
                    .WithMany(p => p.RelatedRelationships)
                    .HasForeignKey(d => d.RelatedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Related");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("Id").UseIdentityColumn();

                entity.Property(e => e.AccountNum).HasColumnName("AccountNum")
                    .IsRequired()
                    .HasColumnName("AccountNum")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("BirthDate")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("UserName")
                    .HasMaxLength(256);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("Gender");

                entity.Property(e => e.ReceivePromotionalEmails)
                    .IsRequired()
                    .HasColumnName("ReceivePromotionalEmails")
                    .HasDefaultValue(false);

                // allow null
                entity.Property(e => e.PreferredGameTypeId)
                    .HasColumnName("PreferredGameTypeId");

                // allow null
                entity.Property(e => e.PreferredPlatformTypeId)
                    .HasColumnName("PreferredPlatformId");

                entity.Property(e => e.WishlistVisibility)
                    .IsRequired()
                    .HasColumnName("WishlistVisibility")
                    .HasDefaultValue(WishlistVisibility.FriendsOnly);

                // foreign key - without navigational property
                modelBuilder.Entity<User>()
                   .HasOne<GameType>()
                   .WithMany()
                   .HasForeignKey(p => p.PreferredGameTypeId)
                   .HasPrincipalKey(p => p.GameTypeId)
                   .HasConstraintName("FK_User_GameType");

                // foreign key - without navigational property
                modelBuilder.Entity<User>()
                   .HasOne<PlatformType>()
                   .WithMany()
                   .HasForeignKey(p => p.PreferredPlatformTypeId)
                   .HasPrincipalKey(p => p.PlatformTypeId)
                   .HasConstraintName("FK_User_PlatformType");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
                entity.HasKey(key => new { key.UserId, key.RoleId });
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
                entity.HasKey(key => new { key.UserId });
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
                entity.HasKey(key => new { key.UserId, key.ProviderKey, key.LoginProvider });
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable(name: "RoleClaims");
                entity.HasKey(key => new { key.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable(name: "UserTokens");
                entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
            });

            Seed(modelBuilder);
        }
    }
}
