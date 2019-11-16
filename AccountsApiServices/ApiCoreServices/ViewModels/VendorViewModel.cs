using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCoreServices.ViewModels
{
    public class VendorViewModel
    {
        public long id { get; set; }
        public string nickName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        
        public string referredBy { get; set; }

        public List<VendorDetails> VendorAddressList { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public string modifiedBy { get; set; }

        public string formattedCreatedDate { get; set; }
        public string formattedModifiedDate { get; set; }

        // Address
        public string alternateMobile { get; set; }
        public string homePhone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }

    public class VendorDetails
    {
        public string alternateMobile { get; set; }
        public string homePhone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }

}