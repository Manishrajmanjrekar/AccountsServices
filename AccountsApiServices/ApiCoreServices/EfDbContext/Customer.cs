﻿using System;
using System.Collections.Generic;

namespace ApiCoreServices.EfDbContext
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerDetails = new HashSet<CustomerDetails>();
            CustomerPayments = new HashSet<CustomerPayments>();
            Sales = new HashSet<Sales>();
            SalesReturns = new HashSet<SalesReturns>();
        }

        public long CustId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NickName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string ReferredBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Url { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<CustomerDetails> CustomerDetails { get; set; }
        public virtual ICollection<CustomerPayments> CustomerPayments { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
        public virtual ICollection<SalesReturns> SalesReturns { get; set; }
    }
}