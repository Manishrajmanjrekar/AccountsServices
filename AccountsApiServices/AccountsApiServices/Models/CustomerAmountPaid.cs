using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountsApiServices.Models
{
    public class CustomerAmountPaid
    {
        public int Id { get; set; }
        public int AmountPaid { get; set; }
        public int CustomerId { get; set; }
        public string CreatedDate { get; set; }
        public string Comments { get; set; }
        public string CustomerName { get; set; }

    }
}