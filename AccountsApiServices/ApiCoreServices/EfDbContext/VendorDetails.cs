﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace ApiCoreServices.EfDbContext
{
    public partial class VendorDetails
    {
        public long VendorDetailId { get; set; }
        public long? VendorId { get; set; }
        public string HomePhone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AlternateMobile { get; set; }
        public string Email { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}