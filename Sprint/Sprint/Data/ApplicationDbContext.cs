using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sprint.Models;
using System;

namespace Sprint.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

                entity.Property(p => p.ReviewId).UseIdentityColumn();

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.Property(e => e.GameId).HasColumnName("GameId");

                entity.Property(e => e.Rating).HasColumnName("Rating")
                    .IsRequired();

                entity.HasIndex(b => new { b.UserId, b.GameId })
                    .IsUnique();

                entity.Property(e => e.ReviewContent)
                    .IsRequired()
                    .HasColumnName("reviewContent")
                    .IsUnicode(false);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Game");
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
    }
}
