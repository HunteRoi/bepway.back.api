using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<Coordinates> Coordinates { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Zoning> Zoning { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<ActivitySector>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.IdOpenData)
                    .HasName("company_uk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivitySectorId).HasColumnName("activitySector_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(200);

                entity.Property(e => e.CoordinatesId).HasColumnName("coordinates_id");

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
                    .IsRequired()
                    .HasColumnName("idOpenData")
                    .HasMaxLength(100);

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("imageURL")
                    .HasMaxLength(100);

                entity.Property(e => e.IsPremium).HasColumnName("isPremium");

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

                entity.HasOne(d => d.Coordinates)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CoordinatesId)
                    .HasConstraintName("companyCoordinates_fk");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Company)
                    .HasPrincipalKey(p => p.Login)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("companyCreator_fk");
            });

            modelBuilder.Entity<Coordinates>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Latitude)
                    .IsRequired()
                    .HasColumnName("latitude")
                    .HasMaxLength(50);

                entity.Property(e => e.Longitude)
                    .IsRequired()
                    .HasColumnName("longitude")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Login)
                    .HasName("user_uk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birthDate")
                    .HasColumnType("date");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(200);

                entity.Property(e => e.IsEnabled).HasColumnName("isEnabled");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(200);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("roles")
                    .HasMaxLength(500);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .HasColumnName("rowVersion")
                    .IsRowVersion();

                entity.Property(e => e.TodoList)
                    .HasColumnName("todoList")
                    .HasMaxLength(500);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.InverseCreator)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("userCreator_fk");
            });

            modelBuilder.Entity<Zoning>(entity =>
            {
                entity.HasIndex(e => e.IdOpenData)
                    .HasName("zoning_openData_uk")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("zoning_uk")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoordinatesId).HasColumnName("coordinates_id");

                entity.Property(e => e.IdOpenData)
                    .IsRequired()
                    .HasColumnName("idOpenData")
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(200);

                entity.HasOne(d => d.Coordinates)
                    .WithMany(p => p.Zoning)
                    .HasForeignKey(d => d.CoordinatesId)
                    .HasConstraintName("zoningCoordinates_fk");
            });
        }
    }
}
