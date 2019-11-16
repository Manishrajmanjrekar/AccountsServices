using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCoreServices.ViewModels
{
    public class CustomerViewModel
    {
        public int id { get; set; }
        public string nickName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }

        public  List<CustomerDetails> CustAddressList { get; set; }
        public string referredBy { get; set; }
        public string createdDate { get; set; }
        public string createdBy { get; set; }
        public string modifiedDate { get; set; }
        public string modifiedBy { get; set; }
    }

    public class CustomerDetails
    {
        public string alternateMobile { get; set; }
        public string homePhone { get; set; }
        public string officePhone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string shopName { get; set; }
        public string shopLocation { get; set; }
       
    }
}