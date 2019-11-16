using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreServices.EfDbContext;
using ApiCoreServices.Models;
using ApiCoreServices.ServicesModels;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreServices.SqlLayerInterfaces.StockIn
{
    public class StockInRepository : IStockInRepository
    {
        AccountdbContext _dbContext;
        public StockInRepository(AccountdbContext dbContext)
        {
              _dbContext = dbContext;
        }

        public List<SalesViewModel> LoadNames(AutoCompleteModel data)
        {
            var SalesList = new List<SalesViewModel>();
            try
            {
               
                SalesList = (from stock in _dbContext.StockIn
                                    from sales in _dbContext.Sales
                                    from vendor in _dbContext.Vendor
                                    from cust in _dbContext.Customer
                                    where stock.LoadName.ToLower().Equals(data.q.ToLower())
                                    && stock.StockId.Equals(sales.StockIn) &&
                                    stock.VendorId.Equals(vendor.VendorId) &&
                                    sales.CustId.Equals(cust.CustId)
                                    select new SalesViewModel
                                    {
                                        VendorId= HelperUtility.ConvertLongToInt(vendor.VendorId),
                                        StockInId = HelperUtility.ConvertLongToInt(stock.StockId),
                                        CustomerId = HelperUtility.ConvertLongToInt(cust.CustId),
                                        CustomerName = HelperUtility.GetCustName(cust),
                                        LoadName = stock.LoadName,
                                        Id= HelperUtility.ConvertLongToInt(sales.SalesId),
                                        Quantity= sales.Quantity,
                                        Total = sales.Total,
                                        Price = sales.Price,
                                        CreatedDate = sales.CreatedDate.ToString(),
                                        VendorName = HelperUtility.GetVendorName(vendor),

                                    }).ToList();


               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SalesList;
        }

        public SalesViewModel StockById(string id)
        {
            var stockid = Convert.ToInt64(id);

            var stockInfo = (from s in _dbContext.StockIn
                             from v in _dbContext.Vendor
                             where s.StockId.Equals(stockid) && s.VendorId.Equals(v.VendorId)
                             select new SalesViewModel
                             {

                                 StockInId = HelperUtility.ConvertLongToInt(s.StockId),
                                 LoadName = s.LoadName,
                                 Id = HelperUtility.ConvertLongToInt(s.StockId),
                                 Quantity = s.Quantity,
                                 CreatedDate = s.CreatedDate.ToString(),
                                 VendorName = HelperUtility.GetVendorName(v),

                             }).FirstOrDefault();


            return stockInfo;

        }

        public int StockInCount(StockInViewModel stock)
        {
            var countOfRecoreds =_dbContext.StockIn.Where(e => e.StockId.Equals(stock.Id)).Count();
            return countOfRecoreds;
        }
        public StockInViewModel AddStock(StockInViewModel data)
        {
            try
            {
                _dbContext.StockIn.Add(ConstructStockInAddressAsPerContext(data));
                _dbContext.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }

            return data;

        }

        public List<StockInViewModel> GetActiveStockList()
        {
            var stockList = (from s in _dbContext.StockIn
                             where s.IsActive.Equals(true)
                             select new StockInViewModel
                             {
                                 Id= HelperUtility.ConvertLongToInt(s.StockId),
                                 loadName = s.LoadName,
                                 VendorId = HelperUtility.ConvertLongToInt(s.VendorId),
                                 TotalQuantity =s.Quantity

                             }).ToList();

            return stockList;
        }

        public List<StockInViewModel> GetAllStockList()
        {
            var stockList = (from s in _dbContext.StockIn
                             where s.IsActive.Equals(true)
                             select new StockInViewModel
                             {
                                 Id = HelperUtility.ConvertLongToInt(s.StockId),
                                 loadName = s.LoadName,
                                 VendorId = HelperUtility.ConvertLongToInt(s.VendorId),
                                 TotalQuantity = s.Quantity,
                                 IsActive = Convert.ToBoolean(s.IsActive)
                                 
                             }).ToList();

            return stockList;
        }

        public List<StockInViewModel> GetStockBetweenDates(string startDate,string endDate)
        {
            var stockList = (from s in _dbContext.StockIn
                             where s.CreatedDate >= Convert.ToDateTime(startDate)
                             where s.CreatedDate <= Convert.ToDateTime(endDate)
                             select new StockInViewModel
                             {
                                 Id = HelperUtility.ConvertLongToInt(s.StockId),
                                 loadName = s.LoadName,
                                 VendorId = HelperUtility.ConvertLongToInt(s.VendorId),
                                 TotalQuantity = s.Quantity,
                                 IsActive = Convert.ToBoolean(s.IsActive)

                             }).ToList();

            return stockList;
        }

        public bool CheckIsDuplicateNickName([FromBody] string data)
        {
            if (_dbContext.StockIn.Any(x => x.LoadName.ToLower() == data.ToLower() && x.IsActive == true))
            {
                return true;
            }

            return false;
        }
        public bool DeleteVendorById(int id)
        {
            var stockInfo = _dbContext.StockIn.Where(e => e.StockId.Equals(id)).FirstOrDefault();
            stockInfo.IsActive = false;

            _dbContext.StockIn.Update(stockInfo);
            _dbContext.SaveChanges();
            return true;

        }

        private  EfDbContext.StockIn ConstructStockInAddressAsPerContext(StockInViewModel item)
        {
            return new EfDbContext.StockIn
            {
                VendorId = HelperUtility.ConvertLongToInt(item.VendorId),
                LoadName = item.loadName,
                Quantity = item.TotalQuantity,
                UpdatedDate = DateTime.Today,
                
            };
        }

       

        
    }
}
