using System;
using System.Collections.Generic;

namespace ApiCoreServices.EfDbContext
{
    public partial class StockIn
    {
        public StockIn()
        {
            CommissionEarned = new HashSet<CommissionEarned>();
            Sales = new HashSet<Sales>();
            SalesReturns = new HashSet<SalesReturns>();
            VendorExpenses = new HashSet<VendorExpenses>();
            VendorPayments = new HashSet<VendorPayments>();
        }

        public long StockId { get; set; }
        public long VendorId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LoadName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LoginId { get; set; }
        public bool? IsActive { get; set; }
        public int Quantity { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<CommissionEarned> CommissionEarned { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
        public virtual ICollection<SalesReturns> SalesReturns { get; set; }
        public virtual ICollection<VendorExpenses> VendorExpenses { get; set; }
        public virtual ICollection<VendorPayments> VendorPayments { get; set; }
    }
}