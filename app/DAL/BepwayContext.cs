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

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressTranslation> AddressTranslation { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyTranslation> CompanyTranslation { get; set; }
        public virtual DbSet<Creation> Creation { get; set; }
        public virtual DbSet<GeoCoordinates> GeoCoordinates { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Road> Road { get; set; }
        public virtual DbSet<RoadGeoreference> RoadGeoreference { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Zoning> Zoning { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");
            modelBuilder.HasDefaultSchema("bepway");
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(1);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(1);

                entity.Property(e => e.FloorNumber)
                    .HasColumnName("floorNumber")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.PostalBox)
                    .IsRequired()
                    .HasColumnName("postalBox")
                    .HasMaxLength(1);

                entity.Property(e => e.RoadId)
                    .HasColumnName("road_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zipCode")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Road)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.RoadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("addressRoad");
            });

            modelBuilder.Entity<AddressTranslation>(entity =>
            {
                entity.ToTable("AddressTranslation", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(1);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.AddressTranslation)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("addressTransleted");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CompanyId)
                    .HasColumnName("company_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.EditDate)
                    .HasColumnName("editDate")
                    .HasColumnType("date");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(1);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auditCompany");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auditUser");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("imageURL")
                    .HasMaxLength(1);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1);

                entity.Property(e => e.Sector)
                    .IsRequired()
                    .HasColumnName("sector")
                    .HasMaxLength(1);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(1);

                entity.Property(e => e.UrlSite)
                    .HasColumnName("urlSite")
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<CompanyTranslation>(entity =>
            {
                entity.ToTable("CompanyTranslation", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ActivitySector)
                    .IsRequired()
                    .HasColumnName("activitySector")
                    .HasMaxLength(1);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("company_id")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyTranslation)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("companyTranslated");
            });

            modelBuilder.Entity<Creation>(entity =>
            {
                entity.ToTable("Creation", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CompanyId)
                    .HasColumnName("company_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creationDate")
                    .HasColumnType("date");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(1);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Creation)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("creationCompany");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Creation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("creationUser");
            });

            modelBuilder.Entity<GeoCoordinates>(entity =>
            {
                entity.HasKey(e => new { e.Latitude, e.Longitude })
                    .HasName("geoCoordinatesKey");

                entity.ToTable("GeoCoordinates", "bepway");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("company_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.EndDate)
                    .HasColumnName("endDate")
                    .HasColumnType("date");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historyAddress");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historyCompany");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<Road>(entity =>
            {
                entity.ToTable("Road", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsPracticable).HasColumnName("isPracticable");

                entity.Property(e => e.ZoningId)
                    .HasColumnName("zoning_id")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Zoning)
                    .WithMany(p => p.Road)
                    .HasForeignKey(d => d.ZoningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("roadZoning");
            });

            modelBuilder.Entity<RoadGeoreference>(entity =>
            {
                entity.HasKey(e => new { e.Latitude, e.Longitude, e.RoadId })
                    .HasName("roadGeoreferenceKey");

                entity.ToTable("RoadGeoreference", "bepway");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.RoadId)
                    .HasColumnName("road_id")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Road)
                    .WithMany(p => p.RoadGeoreference)
                    .HasForeignKey(d => d.RoadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("roadRoadGeoreference");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("userKey");

                entity.ToTable("User", "bepway");

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(1)
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.Creator)
                    .HasColumnName("creator")
                    .HasMaxLength(1);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(1);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.IsEnabled).HasColumnName("isEnabled");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(1);

                entity.Property(e => e.TodoList)
                    .HasColumnName("todoList")
                    .HasMaxLength(1);

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.InverseCreatorNavigation)
                    .HasForeignKey(d => d.Creator)
                    .HasConstraintName("creatorKey");
            });

            modelBuilder.Entity<Zoning>(entity =>
            {
                entity.ToTable("Zoning", "bepway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1);
            });
        }
    }
}
