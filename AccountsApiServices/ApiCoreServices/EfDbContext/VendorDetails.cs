using System;
using System.Collections.Generic;

namespace ApiCoreServices.EfDbContext
{
    public partial class VendorDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string HomePhone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string NickName { get; set; }
        public string ReferredBy { get; set; }
        public string AlternateMobile { get; set; }
        public string Email { get; set; }
    }
}