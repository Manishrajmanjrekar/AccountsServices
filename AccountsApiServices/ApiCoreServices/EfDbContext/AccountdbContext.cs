using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

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

        public virtual DbSet<CommissionAgentExpenses> CommissionAgentExpenses { get; set; }
        public virtual DbSet<CommissionAgentPurchases> CommissionAgentPurchases { get; set; }
        public virtual DbSet<CommissionAgentSales> CommissionAgentSales { get; set; }
        public virtual DbSet<CommissionEarned> CommissionEarned { get; set; }
        public virtual DbSet<CommissionPercentage> CommissionPercentage { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerDetails> CustomerDetails { get; set; }
        public virtual DbSet<CustomerPayments> CustomerPayments { get; set; }
        public virtual DbSet<ExpenseTypes> ExpenseTypes { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<SalesReturns> SalesReturns { get; set; }
        public virtual DbSet<StockIn> StockIn { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<VendorAdvances> VendorAdvances { get; set; }
        public virtual DbSet<VendorAdvancesReturned> VendorAdvancesReturned { get; set; }
        public virtual DbSet<VendorDetails> VendorDetails { get; set; }
        public virtual DbSet<VendorExpenses> VendorExpenses { get; set; }
        public virtual DbSet<VendorPayments> VendorPayments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //    }
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("AccountsDBConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<CommissionAgentExpenses>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(500);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Expense)
                    .WithMany(p => p.CommissionAgentExpenses)
                    .HasForeignKey(d => d.ExpenseId)
                    .HasConstraintName("FK_CommisionAgentExpenses_Expenses");
            });

            modelBuilder.Entity<CommissionAgentPurchases>(entity =>
            {
                entity.HasKey(e => e.CommisionAgentPurchasesId);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoggedInUser).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.CommissionAgentPurchases)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_CommissionAgentPurchases_Vendor");
            });

            modelBuilder.Entity<CommissionAgentSales>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoggedInUser).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.CommissionAgentSales)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_CommissionAgentSales_Vendor");
            });

            modelBuilder.Entity<CommissionEarned>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(500);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CommissionPercentage)
                    .WithMany(p => p.CommissionEarned)
                    .HasForeignKey(d => d.CommissionPercentageId)
                    .HasConstraintName("FK_CommissionEarned_CommissionPercentage");

                entity.HasOne(d => d.StockIn)
                    .WithMany(p => p.CommissionEarned)
                    .HasForeignKey(d => d.StockInId)
                    .HasConstraintName("FK_CommissionEarned_StockIn");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasColumnName("lastName");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(500);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NickName).HasColumnName("nickName");

                entity.Property(e => e.ReferredBy).HasColumnName("referredBy");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<CustomerDetails>(entity =>
            {
                entity.HasKey(e => e.CustAddressId)
                    .HasName("PK_CustomerAddress");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.AlternateMobile)
                    .HasColumnName("alternateMobile")
                    .HasMaxLength(50);

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.HomePhone)
                    .HasColumnName("homePhone")
                    .HasMaxLength(50);

                entity.Property(e => e.OfficePhone)
                    .HasColumnName("officePhone")
                    .HasMaxLength(50);

                entity.Property(e => e.ShopLocation).HasColumnName("shopLocation");

                entity.Property(e => e.ShopName).HasColumnName("shopName");

                entity.Property(e => e.State).HasColumnName("state");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.CustomerDetails)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK_CustomerAddress_Customer");
            });

            modelBuilder.Entity<CustomerPayments>(entity =>
            {
                entity.HasKey(e => e.CustomerPaymentId);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoggedInUser).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.CustomerPayments)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK_CustomerPayments_Customer");
            });

            modelBuilder.Entity<ExpenseTypes>(entity =>
            {
                entity.HasKey(e => e.ExpenseTypeId);
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.HasKey(e => e.ExpenseId)
                    .HasName("PK_Table_1");

                entity.HasOne(d => d.ExpenseType)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.ExpenseTypeId)
                    .HasConstraintName("FK_Expenses_ExpenseTypes");
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LoggedInUser).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK_Sales_Customer");

                entity.HasOne(d => d.StockIn)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.StockInId)
                    .HasConstraintName("FK_Sales_StockIn");
            });

            modelBuilder.Entity<SalesReturns>(entity =>
            {
                entity.HasKey(e => e.SalesReturnId);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoggedInUser).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK_SalesReturns_StockIn");

                entity.HasOne(d => d.StockIn)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.StockInId)
                    .HasConstraintName("FK_SalesReturns_StockIn1");
            });

            modelBuilder.Entity<StockIn>(entity =>
            {
                entity.HasKey(e => e.StockId);

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LoadName).IsRequired();

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.StockIn)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockIn_VendorDetails");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(500);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middleName")
                    .HasMaxLength(500);

                entity.Property(e => e.MobileNo)
                    .HasColumnName("mobileNo")
                    .HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NickName)
                    .HasColumnName("nickName")
                    .HasMaxLength(500);

                entity.Property(e => e.ReferredBy)
                    .HasColumnName("referredBy")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<VendorAdvances>(entity =>
            {
                entity.HasKey(e => e.AdvancesId);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.VendorAdvances)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_VendorAdvances_Vendor");
            });

            modelBuilder.Entity<VendorAdvancesReturned>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoggedInUser).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.VendorAdvancesReturned)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_VendorAdvancesReturned_Vendor");
            });

            modelBuilder.Entity<VendorDetails>(entity =>
            {
                entity.HasKey(e => e.VendorDetailId)
                    .HasName("PK_VendorDetails_1");

                entity.Property(e => e.VendorDetailId).ValueGeneratedNever();

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

                entity.Property(e => e.HomePhone)
                    .HasColumnName("homePhone")
                    .HasMaxLength(500);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(500);

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.VendorDetails)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_VendorDetails_Vendor");
            });

            modelBuilder.Entity<VendorExpenses>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(500);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Expense)
                    .WithMany(p => p.VendorExpenses)
                    .HasForeignKey(d => d.ExpenseId)
                    .HasConstraintName("FK_VendorExpenses_Expenses");

                entity.HasOne(d => d.StockIn)
                    .WithMany(p => p.VendorExpenses)
                    .HasForeignKey(d => d.StockInId)
                    .HasConstraintName("FK_VendorExpenses_StockIn");
            });

            modelBuilder.Entity<VendorPayments>(entity =>
            {
                entity.HasKey(e => e.VendorPaymentId);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoggedInUser).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.StockIn)
                    .WithMany(p => p.VendorPayments)
                    .HasForeignKey(d => d.StockInId)
                    .HasConstraintName("FK_VendorPayments_StockIn");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.VendorPayments)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK_VendorPayments_Vendor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}