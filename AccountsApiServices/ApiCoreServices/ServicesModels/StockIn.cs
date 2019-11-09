using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCoreServices.Models
{
    public class StockIn
    {
        public int Id { get; set; }
        public string nickName { get; set; }
        public string firstName { get; set; }

        public int TotalQuantity { get; set; }
        public string CreatedDate { get; set; }
        public int VendorId { get; set; }
        public bool IsActive { get; set; }
        public string loadName { get; set; }





    }
}