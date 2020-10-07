using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sprint.Models
{
    public partial class game_storeContext : DbContext
    {
        public game_storeContext()
        {
        }

        public game_storeContext(DbContextOptions<game_storeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GameType> GameType { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MsSqlLocalDb;Database=game_store;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameType>(entity =>
            {
                entity.ToTable("gameType");

                entity.Property(e => e.GameTypeId).HasColumnName("gameTypeId");

                entity.Property(e => e.GameType1)
                    .IsRequired()
                    .HasColumnName("gameType")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.ToTable("games");

                entity.Property(e => e.GameId).HasColumnName("gameId");

                entity.Property(e => e.Developer)
                    .IsRequired()
                    .HasColumnName("developer")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.GameName)
                    .IsRequired()
                    .HasColumnName("gameName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.GameTypeId).HasColumnName("gameTypeId");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.GameType)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GameTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_games_gameType");
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GameId })
                    .HasName("PK_reviews_1");

                entity.ToTable("reviews");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.GameId).HasColumnName("gameId");

                entity.Property(e => e.ReviewContent)
                    .IsRequired()
                    .HasColumnName("reviewContent")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("userType");

                entity.Property(e => e.UserTypeId).HasColumnName("userTypeId");

                entity.Property(e => e.UserType1)
                    .IsRequired()
                    .HasColumnName("userType")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.AccountNum).HasColumnName("accountNum");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birthDate")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("fName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasColumnName("lName")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasColumnName("phoneNum")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.UserTypeId).HasColumnName("userTypeId");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_userType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
