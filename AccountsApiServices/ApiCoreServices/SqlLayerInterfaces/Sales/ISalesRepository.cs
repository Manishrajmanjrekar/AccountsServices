using ApiCoreServices.Models;
using ApiCoreServices.ServicesModels;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreServices.SqlLayerInterfaces
{
    public interface ISalesRepository
    {
        List<SalesViewModel> LoadCustNames(AutoCompleteModel data);
        List<SalesViewModel> LoadVendorNames(AutoCompleteModel data);

        SalesViewModel AddSales(SalesViewModel data);

        bool AddMultipleSales(List<SalesViewModel> data);

        SalesViewModel UpdateSales(SalesViewModel data);

        List<SalesViewModel> GetSalesBySalesId(string id);

        List<SalesViewModel> GetSalesByStockInId(string id);

        List<SalesViewModel> GetSalesByVendorId(string id);

        List<SalesViewModel> GetSalesByCustomerId(string id);

        int TotalStockSold(string id);

        List<SalesViewModel> GetSalesBetweenDates(string startDate, string endDate);

       

       
    }
}
