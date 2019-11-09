using System;
using System.Collections.Generic;

namespace ApiCoreServices.EfDbContext
{
    public partial class StockIn
    {
        public int Id { get; set; }
        public int? VendorId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LoadName { get; set; }
    }
}