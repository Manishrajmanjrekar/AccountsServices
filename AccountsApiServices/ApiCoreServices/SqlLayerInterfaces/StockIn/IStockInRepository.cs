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
    public interface IStockInRepository
    {
        List<SalesViewModel> LoadNames(AutoCompleteModel data);

        SalesViewModel StockById(string id);

        int StockInCount(StockInViewModel stock);

        StockInViewModel AddStock(StockInViewModel data);

        List<StockInViewModel> GetActiveStockList();

        List<StockInViewModel> GetAllStockList();

        List<StockInViewModel> GetStockBetweenDates(string startDate, string endDate);

        bool CheckIsDuplicateNickName([FromBody] string data);

        bool DeleteVendorById(int id);
    }
}
