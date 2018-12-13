using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Model;

using Model;

namespace DAL
{
    public partial class BepwayContext : DbContext
    {
        public BepwayContext()
        {
        }

        public BepwayContext(DbContextOptions<BepwayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivitySector> ActivitySector { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:DefaultSchema", "db_owner");

            modelBuilder.Entity<ActivitySector>(entity =>
            {
                entity.ToTable("ActivitySector", "dbo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company", "dbo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ActivitySectorId)
                    .HasColumnName("activitySector_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(200);

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creationDate")
                    .HasColumnType("date");

                entity.Property(e => e.CreatorId)
                    .HasColumnName("creator_id")
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.IdOpenData)
                    .HasColumnName("idOpenData")
                    .HasMaxLength(100);

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("imageURL")
                    .HasMaxLength(100);

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .HasColumnName("rowVersion")
                    .IsRowVersion();

                entity.Property(e => e.SiteUrl)
                    .HasColumnName("siteURL")
                    .HasMaxLength(200);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50);

                entity.HasOne(d => d.ActivitySector)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.ActivitySectorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("activitySector_fk");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("companyCreator_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("user_pk");

                entity.ToTable("User", "dbo");

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.Creator)
                    .HasColumnName("creator")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(200);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.IsEnabled).HasColumnName("isEnabled");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(200);

                entity.Property(e => e.TodoList)
                    .HasColumnName("todoList")
                    .HasMaxLength(500);

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.InverseCreatorNavigation)
                    .HasForeignKey(d => d.Creator)
                    .HasConstraintName("userCreator_fk");
            });
        }
    }
}
