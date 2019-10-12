using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountsApiServices.Models
{
    public class StockIn
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public int TotalQuantity { get; set; }
        public string CreatedDate { get; set; }
        public int VendorId { get; set; }
        public bool IsActive { get; set; }





    }
}