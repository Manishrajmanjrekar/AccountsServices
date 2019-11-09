using Models;
using System.Collections.Generic;

namespace ServiceLayer
{
    public interface IVendorService
    {
        List<Vendor> GetVendorNames();
    }
}