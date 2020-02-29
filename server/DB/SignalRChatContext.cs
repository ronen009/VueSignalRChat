using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace server.DB
{
    public partial class SignalRChatContext : DbContext
    {
        public SignalRChatContext()
        {
        }

        public SignalRChatContext(DbContextOptions<SignalRChatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<SignInCodes> SignInCodes { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SignalRChat;Trusted_Connection=True;");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.UsersChatId)
                    .HasName("PK__Messages__6993B30FB48CF188");

                entity.Property(e => e.Msg)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.HasOne(d => d.MsgFromUser)
                    .WithMany(p => p.MessagesMsgFromUser)
                    .HasForeignKey(d => d.MsgFromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MsgFromUser");

                entity.HasOne(d => d.MsgToUser)
                    .WithMany(p => p.MessagesMsgToUser)
                    .HasForeignKey(d => d.MsgToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MsgToUser");
            });

            modelBuilder.Entity<SignInCodes>(entity =>
            {
                entity.HasKey(e => e.CodeId)
                    .HasName("PK__SignInCo__C6DE2C153AB11B2F");

                entity.HasIndex(e => e.UserId)
                    .HasName("UQ__SignInCo__1788CC4D2F4BEB95")
                    .IsUnique();

                entity.HasIndex(e => new { e.UserId, e.Code })
                    .HasName("ix_nonC_SignInCodes_UserId_Code");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.SignInCodes)
                    .HasForeignKey<SignInCodes>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SignInCod__UserI__2C3393D0");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C2F3C7BF7");

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Users__C9F28456B458E140")
                    .IsUnique();

                entity.HasIndex(e => new { e.UserName, e.Phone })
                    .HasName("ix_nonC_Users_UserName_Phone");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
