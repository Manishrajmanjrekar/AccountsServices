using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreServices
{
    public class HelperUtility
    {
        public static int ConvertLongToInt(long id)
        {
            return unchecked((int)id);
        }

        public static string GetCustName(EfDbContext.Customer cust)
        {
            return cust.FirstName + " " + cust.LastName;
        }

        public static string GetVendorName(EfDbContext.Vendor vendor)
        {
            return vendor.FirstName + " " + vendor.LastName;
        }

        
    }
}
