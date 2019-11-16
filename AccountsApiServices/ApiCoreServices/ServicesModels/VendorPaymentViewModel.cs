using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreServices.ServicesModels
{
    public class VendorPaymentViewModel
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int StockInId { get; set; }
       
        public int AmountPaid { get; set; }
        public string CreatedDate { get; set; }
    }
}
