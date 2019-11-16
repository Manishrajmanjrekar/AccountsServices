using ApiCoreServices.Models;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreServices.SqlLayerInterfaces
{
    public interface IVendorRepository
    {
         CommonResponseViewModel SaveVendor(VendorViewModel customerVM);

        VendorViewModel GetVendorById(int id);

        List<VendorViewModel> VendorNames(AutoCompleteModel data);

        List<VendorViewModel> GetAllVendors();

        bool CheckIsDuplicateNickName([FromBody]string data);

        bool DeleteVendorById(int id);
    }
}
