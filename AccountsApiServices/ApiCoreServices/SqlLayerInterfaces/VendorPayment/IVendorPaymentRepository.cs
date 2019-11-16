using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreServices.Models;
using ApiCoreServices.ServicesModels;
using ApiCoreServices.ViewModels;

namespace ApiCoreServices.SqlLayerInterfaces.VendorPayment
{
   

    public interface IVendorPaymentRepository
    {
        CommonResponseViewModel SaveVendorPayment(VendorPaymentViewModel vendorPaymentVM);

        VendorViewModel GetVendorById(int id);

        List<VendorViewModel> VendorNames(AutoCompleteModel data);

        List<VendorViewModel> GetAllVendors();

        bool CheckIsDuplicateNickName(string data);

        bool DeleteVendorById(int id);
    }
}
