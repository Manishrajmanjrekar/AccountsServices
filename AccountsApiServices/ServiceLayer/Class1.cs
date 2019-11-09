using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ServiceLayer
{
    public class SqlVendorService : IVendorService
    {
        public SqlVendorService()
        {

        }
        public List<Vendor> GetVendorNames()
        {
            throw new NotImplementedException();
        }
    }
}
