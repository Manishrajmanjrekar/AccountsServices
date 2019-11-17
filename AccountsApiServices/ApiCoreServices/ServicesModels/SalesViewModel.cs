using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreServices.ServicesModels
{
    public class SalesViewModel
    {
        public long Id { get; set; }
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long StockInId { get; set; }
        public string LoadName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public string CreatedDate { get; set; }


    }
}
