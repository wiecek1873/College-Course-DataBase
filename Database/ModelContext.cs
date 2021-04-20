using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Bazadanych
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Votetime> Votetimes { get; set; }
        public virtual DbSet<Votetopic> Votetopics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User Id=BAZADANYCH;Password=123456;Data Source=127.0.0.1:1521/xe;",options => options.UseOracleSQLCompatibility("11"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BAZADANYCH");

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("option");

                entity.Property(e => e.Optionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("OPTIONID");

                entity.Property(e => e.Information)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("INFORMATION");

                entity.Property(e => e.Optiongroupid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("OPTIONGROUPID");

                entity.Property(e => e.Votes)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VOTES");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("permissions");

                entity.Property(e => e.Canvote)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CANVOTE");

                entity.Property(e => e.Topicsid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOPICSID");

                entity.Property(e => e.Usersid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERSID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.Property(e => e.Emailadress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("EMAILADRESS");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");
            });

            modelBuilder.Entity<Votetime>(entity =>
            {
                entity.ToTable("votetime");

                entity.Property(e => e.Votetimeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VOTETIMEID");

                entity.Property(e => e.Votestarttime)
                    .HasPrecision(6)
                    .HasColumnName("VOTESTARTTIME");

                entity.Property(e => e.Votestoptime)
                    .HasPrecision(6)
                    .HasColumnName("VOTESTOPTIME");
            });

            modelBuilder.Entity<Votetopic>(entity =>
            {
                entity.ToTable("votetopic");

                entity.Property(e => e.Votetopicid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VOTETOPICID");

                entity.Property(e => e.Maininformation)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("MAININFORMATION");

                entity.Property(e => e.Optiongroupid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("OPTIONGROUPID");

                entity.Property(e => e.Votetimeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VOTETIMEID");

                entity.HasOne(d => d.Optiongroup)
                    .WithMany(p => p.Votetopics)
                    .HasForeignKey(d => d.Optiongroupid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VOTETIME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
