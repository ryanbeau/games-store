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
        public virtual DbSet<GameDiscount> GameDiscounts { get; set; }
        public virtual DbSet<GameImage> GameImages { get; set; }
        public virtual DbSet<GameType> GameTypes { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<UserGameWishlist> UserGameWishlists { get; set; }
        public virtual DbSet<UserRelationship> UserRelationships { get; set; }

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

        private void Seed(ModelBuilder modelBuilder)
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
                new GameDiscount { DiscountId = 6, GameId = 5, DiscountPrice = 11.59m, DiscountStart = new DateTime(2020, 11, 08), DiscountFinish = new DateTime(2020, 12, 25, 23, 0, 0) }
            );

            modelBuilder.Entity<GameImage>().HasData(
                // Euro Truck Simulator 2
                new GameImage { GameImageId = 1, GameId = 1, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/4c22bd444899d3b6047a10b20a2f26db.png" },
                // Madden NFL 20
                new GameImage { GameImageId = 2, GameId = 2, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/097e232de59f809f5a1cdf88e1240b08.png" },
                // Don't Starve
                new GameImage { GameImageId = 3, GameId = 3, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/b16a06a5ea94028944a81ad5bbdbb8ca.png" },
                // Borderlands 3
                new GameImage { GameImageId = 4, GameId = 4, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/7e04e496f1cf3896708f48127a7b65de.png" },
                // Tomb Raider
                new GameImage { GameImageId = 5, GameId = 5, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/6be910cdb73c47cb973a944c03f5c7b1.png" },
                // Fallout: New Vegas
                new GameImage { GameImageId = 6, GameId = 6, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5248e5118c84beea359b6ea385393661.png" },
                // Besiege
                new GameImage { GameImageId = 7, GameId = 7, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/a3171cc0f610fdfdf460831fb25a3dc7.png" },
                // Kerbal Space Program
                new GameImage { GameImageId = 8, GameId = 8, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5e26566dffe850373e9a5121703034a1.png" },
                // Dragon Age: Origins
                new GameImage { GameImageId = 9, GameId = 9, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/c1537c9ed39baee3476c6fdd666b5fd8.png" },
                // Far Cry 3
                new GameImage { GameImageId = 10, GameId = 10, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/84b3f2becbf70a03239d7fae55dcaa40.png" },
                // Far Cry 3 - Blood Dragon
                new GameImage { GameImageId = 11, GameId = 11, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/1051f72ed869290c51ee34a72b1d01df.png" },
                // Minecraft
                new GameImage { GameImageId = 12, GameId = 12, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/6db2fc0f9848c8830f2c5ad73e78ea75.png" },
                // Roblox
                new GameImage { GameImageId = 13, GameId = 13, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/2a574bcb25a0ae1faad7c630370e6234.png" },
                // Super Mario Odyssey
                new GameImage { GameImageId = 14, GameId = 14, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5549f6da5ec3b191b672e682e4735d71.png" },
                // Grand Theft Auto V
                new GameImage { GameImageId = 15, GameId = 15, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/3aa4cb2017fe681acd92bbea6b9f6015.png" },
                // Slime Rancher
                new GameImage { GameImageId = 16, GameId = 16, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5f65c233d57a4b31b1e4edbaa79bf6ca.png" },
                // Rocket League
                new GameImage { GameImageId = 17, GameId = 17, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/ea3a48c74a9efb9a08635fe7990347cc.png" },
                // Mortal Kombat 11
                new GameImage { GameImageId = 18, GameId = 18, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/884738b4332ababd678ca505f4e04f4d.png" },
                // The Legend of Zelda: Breath of the Wild
                new GameImage { GameImageId = 19, GameId = 19, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/2da535ad78bb2e93aa448b1a4a61134e.png" },
                // Call of Duty: Black Ops 4
                new GameImage { GameImageId = 20, GameId = 20, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/cb0fb5b71dd8266417731afb0e7a0864.png" },
                // Destiny 2
                new GameImage { GameImageId = 21, GameId = 21, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/f5e083092550d2f93898e9829e677e39.png" },
                // The Elder Scrolls® Online
                new GameImage { GameImageId = 22, GameId = 22, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/56c12a4512e84416de450db11ab040c3.png" },
                // WRC 3: FIA World Rally Championship
                new GameImage { GameImageId = 23, GameId = 23, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/4a8520defd77a137222438d72ed7afd2.png" },
                // Need for Speed: Carbon
                new GameImage { GameImageId = 24, GameId = 24, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/ee955e252af3c85e66e15864e31174fe.png" },
                // Tropico 5
                new GameImage { GameImageId = 25, GameId = 25, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/c967fb654df41177901d1f5f135bf9e6.png" },
                // Among Us
                new GameImage { GameImageId = 26, GameId = 26, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/75c0aa52af187c4cd20744efafa1c7c7.png" },
                // Red Dead Redemption II
                new GameImage { GameImageId = 27, GameId = 27, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/41b28a11da13a0384a9b75f95244e8e8.png" },
                // Sid Meier's Civilization VI
                new GameImage { GameImageId = 28, GameId = 28, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/c9ee6a825655d889ae6a84bde2802bc2.png" },
                // Horizon Zero Dawn
                new GameImage { GameImageId = 29, GameId = 29, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5ec5d5702a083583b268f32dde14b419.png" },
                // Fallout 4
                new GameImage { GameImageId = 30, GameId = 30, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/60c60a4ffa03bde6f8c83533d465ef5c.png" }
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    .IsRequired();

                entity.Property(e => e.DiscountStart).HasColumnName("DiscountStart")
                    .IsRequired();

                entity.Property(e => e.DiscountFinish).HasColumnName("DiscountFinish")
                    .IsRequired();

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Discount");
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

                entity.Property(e => e.WishlistVisibility)
                    .IsRequired()
                    .HasColumnName("WishlistVisibility")
                    .HasDefaultValue(WishlistVisibility.FriendsOnly);
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
