﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCoreServices.Models
{
    public class VendorPaymentModel
    {
        public List<Sales> SalesList { get; set; }

        public List<VendorLoadExpense> VendorLoadExpenseList { get; set; }
    }
}