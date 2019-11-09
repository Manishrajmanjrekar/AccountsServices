using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiCoreServices.EfDbContext
{
    public partial class AccountdbContext : DbContext
    {
        public AccountdbContext()
        {
        }

        public AccountdbContext(DbContextOptions<AccountdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<StockIn> StockIn { get; set; }
        public virtual DbSet<VendorDetails> VendorDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<StockIn>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LoadName).IsRequired();
            });

            modelBuilder.Entity<VendorDetails>(entity =>
            {
                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.AlternateMobile)
                    .HasColumnName("alternateMobile")
                    .HasMaxLength(500);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(500);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(500);

                entity.Property(e => e.HomePhone)
                    .HasColumnName("homePhone")
                    .HasMaxLength(500);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(500);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middleName")
                    .HasMaxLength(500);

                entity.Property(e => e.MobileNo)
                    .HasColumnName("mobileNo")
                    .HasMaxLength(500);

                entity.Property(e => e.NickName)
                    .HasColumnName("nickName")
                    .HasMaxLength(500);

                entity.Property(e => e.ReferredBy)
                    .HasColumnName("referredBy")
                    .HasMaxLength(500);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}