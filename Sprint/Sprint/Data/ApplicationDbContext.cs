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
        public virtual DbSet<GameImage> GameImages { get; set; }
        public virtual DbSet<GameType> GameTypes { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

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
                    PhoneNumber = "555-555-5555",
                    BirthDate = new DateTime(1970, 01, 01)
                });

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 },
                new IdentityUserRole<int> { RoleId = 2, UserId = 2 }
            );

            modelBuilder.Entity<GameType>().HasData(
                new GameType { GameTypeId = 1, Name = "Action" },
                new GameType { GameTypeId = 2, Name = "Action-Adventure" },
                new GameType { GameTypeId = 3, Name = "Adventure" },
                new GameType { GameTypeId = 4, Name = "Role-Playing" },
                new GameType { GameTypeId = 5, Name = "Simulation" },
                new GameType { GameTypeId = 6, Name = "Strategy" },
                new GameType { GameTypeId = 7, Name = "Sports" },
                new GameType { GameTypeId = 8, Name = "MMO" },
                new GameType { GameTypeId = 9, Name = "Sandbox" }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game { GameId = 1, Name = "Call of Duty: Modern Warfare", Developer = "Activision", GameTypeId = 1 },
                new Game { GameId = 2, Name = "NBA 2K20", Developer = "2K Sports", GameTypeId = 7 },
                new Game { GameId = 3, Name = "Madden NFL 20", Developer = "Electronic Arts", GameTypeId = 7 },
                new Game { GameId = 4, Name = "Borderlands 3", Developer = "2K Games", GameTypeId = 1 },
                new Game { GameId = 5, Name = "Mortal Kombat II", Developer = "Warner Bros. Interactive Entertainment", GameTypeId = 1 },
                new Game { GameId = 6, Name = "Star Wars Jedi: Fallen Order", Developer = "Electronic Arts", GameTypeId = 1 },
                new Game { GameId = 7, Name = "Super Smash Bros", Developer = "Nintendo", GameTypeId = 1 },
                new Game { GameId = 8, Name = "Kingdom Hearts III", Developer = "Square Enix", GameTypeId = 4 },
                new Game { GameId = 9, Name = "Tom Clancy's The Division 2", Developer = "Ubisoft", GameTypeId = 1 },
                new Game { GameId = 10, Name = "Mario Kart 8 Deluxe", Developer = "Nintendo", GameTypeId = 1 },
                new Game { GameId = 11, Name = "Fortnite", Developer = "Epic Games", GameTypeId = 9 },
                new Game { GameId = 12, Name = "Minecraft", Developer = "Mojang", GameTypeId = 9 },
                new Game { GameId = 13, Name = "Roblox", Developer = "Roblox Corporation", GameTypeId = 9 },
                new Game { GameId = 14, Name = "Super Mario Odyssey", Developer = "Nintendo", GameTypeId = 1 },
                new Game { GameId = 15, Name = "Grand Theft Auto V", Developer = "Rockstar Games", GameTypeId = 1 },
                new Game { GameId = 16, Name = "Marvel’s Spider-Man", Developer = "Sony Interactive Entertainment", GameTypeId = 1 },
                new Game { GameId = 17, Name = "Rocket League", Developer = "Psyonix", GameTypeId = 1 },
                new Game { GameId = 18, Name = "League of Legends", Developer = "Riot Games", GameTypeId = 8 },
                new Game { GameId = 19, Name = "The Legend of Zelda: Breath of the Wild", Developer = "Nintendo", GameTypeId = 2 },
                new Game { GameId = 20, Name = "Call of Duty: Black Ops 4", Developer = "Activision", GameTypeId = 1 }
            );

            modelBuilder.Entity<GameImage>().HasData(
                new GameImage { GameImageId = 1, GameId = 1, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/d5c91983451d0fa52a7ce530a3714ab7.png" },
                new GameImage { GameImageId = 2, GameId = 2, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/767b2cc82cecc0385fe6f1086dd2c748.png" },
                new GameImage { GameImageId = 3, GameId = 3, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/94cd0468d6f321ec192c9e301ba30e85.png" },
                new GameImage { GameImageId = 4, GameId = 4, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/7e04e496f1cf3896708f48127a7b65de.png" },
                new GameImage { GameImageId = 5, GameId = 5, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/9a3bd37a71b632e7726f149bbd771052.png" },
                //new GameImage { GameImageId = 6, GameId = 6, ImageType = ImageType.Banner, ImageURL = "" },
                new GameImage { GameImageId = 7, GameId = 7, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/71e9c6620d381d60196ebe694840aaaa.png" },
                //new GameImage { GameImageId = 8, GameId = 8, ImageType = ImageType.Banner, ImageURL = "" },
                new GameImage { GameImageId = 9, GameId = 9, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/29ddbdb402491a6aa97964a8139a1356.png" },
                new GameImage { GameImageId = 10, GameId = 10, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/c6d4eb15f1e84a36eff58eca3627c82e.png" },
                //new GameImage { GameImageId = 11, GameId = 11, ImageType = ImageType.Banner, ImageURL = "" },
                new GameImage { GameImageId = 12, GameId = 12, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/6db2fc0f9848c8830f2c5ad73e78ea75.png" },
                new GameImage { GameImageId = 13, GameId = 13, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/2a574bcb25a0ae1faad7c630370e6234.png" },
                new GameImage { GameImageId = 14, GameId = 14, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/5549f6da5ec3b191b672e682e4735d71.png" },
                //new GameImage { GameImageId = 15, GameId = 15, ImageType = ImageType.Banner, ImageURL = "" },
                //new GameImage { GameImageId = 16, GameId = 16, ImageType = ImageType.Banner, ImageURL = "" },
                new GameImage { GameImageId = 17, GameId = 17, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/ea3a48c74a9efb9a08635fe7990347cc.png" },
                new GameImage { GameImageId = 18, GameId = 18, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/f4a331b7a22d1b237565d8813a34d8ac.png" },
                new GameImage { GameImageId = 19, GameId = 19, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/2da535ad78bb2e93aa448b1a4a61134e.png" },
                new GameImage { GameImageId = 20, GameId = 20, ImageType = ImageType.Banner, ImageURL = "https://cdn.steamgriddb.com/grid/cb0fb5b71dd8266417731afb0e7a0864.png" }
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

                entity.Property(e => e.GameTypeId).HasColumnName("GameTypeId");

                entity.HasOne(d => d.GameType)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GameTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_GameType");
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

        public DbSet<Sprint.Models.UserGameWishlist> UserGameWishlist { get; set; }
    }
}
