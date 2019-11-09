using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class VendorLoadExpense
    {
        public int stockInId { get; set; }
        public int ExpensesCategoryId { get; set; }
        public string ExpensesCategoryName { get; set; }
        public int Amount { get; set; }
        public string CreatedDate { get; set; }
        public string VendorName { get; set; }

    }
}