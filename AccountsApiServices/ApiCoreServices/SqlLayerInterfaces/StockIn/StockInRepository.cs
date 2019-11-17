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
        static string _dateFormat = "dd MMM yyyy";

        public StockInRepository(AccountdbContext dbContext)
        {
              _dbContext = dbContext;
        }

        public List<SalesViewModel> LoadNames(AutoCompleteModel data)
        {
            var SalesList = new List<SalesViewModel>();
            try
            {
                using (_dbContext = new AccountdbContext())
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
                                     VendorId = vendor.VendorId,
                                     StockInId = stock.StockId,
                                     CustomerId = cust.CustId,
                                     CustomerName = HelperUtility.GetCustName(cust),
                                     LoadName = stock.LoadName,
                                     Id = sales.SalesId,
                                     Quantity = sales.Quantity,
                                     Total = sales.Total,
                                     Price = sales.Price,
                                     CreatedDate = sales.CreatedDate.ToString(),
                                     VendorName = HelperUtility.GetVendorName(vendor),

                                 }).ToList();
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SalesList;
        }

        public StockInViewModel StockById(string id)
        {
            var stockid = Convert.ToInt64(id);
            var stockInfo = new StockInViewModel();

            using (_dbContext = new AccountdbContext())
            {
                stockInfo = (from s in _dbContext.StockIn
                             join v in _dbContext.Vendor on s.VendorId equals v.VendorId
                             where s.IsActive.Equals(true) && s.StockId.Equals(stockid)
                             select new StockInViewModel
                             {
                                 id = s.StockId,
                                 loadName = s.LoadName,
                                 vendorId = s.VendorId,
                                 totalQuantity = s.Quantity,
                                 isActive = Convert.ToBoolean(s.IsActive),
                                 createdDate = s.CreatedDate,

                                 formattedCreatedDate = s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString(_dateFormat) : "",
                                 formattedModifiedDate = s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(_dateFormat) : "",
                                 nickName = v.NickName,
                                 firstName = v.FirstName
                             }).FirstOrDefault();
            }

            return stockInfo;
        }

        public int StockInCount(StockInViewModel stock)
        {
            var countOfRecords = 0;
            using (_dbContext = new AccountdbContext())
            {
                countOfRecords = _dbContext.StockIn.Where(e => e.VendorId.Equals(stock.vendorId)).Count();
            }
            return countOfRecords;
        }
        public CommonResponseViewModel AddStock(StockInViewModel stockInVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    var dbStockIn = ConstructStockInAddressAsPerContext(stockInVM);
                    dbStockIn.CreatedDate = stockInVM.createdDate.HasValue ? stockInVM.createdDate.Value : DateTime.Now;
                    _dbContext.StockIn.Add(dbStockIn);
                    _dbContext.SaveChanges();

                    response.recordId = dbStockIn.StockId;
                    response.isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public CommonResponseViewModel UpdateStock(StockInViewModel stockInVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    var dbStockIn = _dbContext.StockIn.Where(s => s.StockId.Equals(stockInVM.id)).FirstOrDefault();
                    if (dbStockIn != null)
                    {
                        dbStockIn.Quantity = stockInVM.totalQuantity;
                        dbStockIn.CreatedDate = stockInVM.createdDate.HasValue ? stockInVM.createdDate.Value : dbStockIn.CreatedDate;
                        dbStockIn.UpdatedDate = DateTime.Now;

                        _dbContext.StockIn.Update(dbStockIn);
                        _dbContext.SaveChanges();

                        response.isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public List<StockInViewModel> GetActiveStockList()
        {
            var stockList = new List<StockInViewModel>();
            using (_dbContext = new AccountdbContext())
            {
                stockList = (from s in _dbContext.StockIn
                 where s.IsActive.Equals(true)
                 select new StockInViewModel
                 {
                     id = s.StockId,
                     loadName = s.LoadName,
                     vendorId = s.VendorId,
                     totalQuantity = s.Quantity

                 }).ToList();
            }

            return stockList;
        }

        public List<StockInViewModel> GetAllStockList()
        {
            var stockList = new List<StockInViewModel>();
            using (_dbContext = new AccountdbContext())
            {
                stockList = (from s in _dbContext.StockIn
                             join v in _dbContext.Vendor on s.VendorId equals v.VendorId
                             where s.IsActive.Equals(true)
                             select new StockInViewModel
                             {
                                 id = s.StockId,
                                 loadName = s.LoadName,
                                 vendorId = s.VendorId,
                                 totalQuantity = s.Quantity,
                                 isActive = Convert.ToBoolean(s.IsActive),
                                 createdDate = s.CreatedDate,

                                 formattedCreatedDate = s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString(_dateFormat) : "",
                                 formattedModifiedDate = s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(_dateFormat) : "",
                                 nickName = v.NickName,
                                 firstName = v.FirstName
                             }).ToList();
            }

            return stockList;
        }

        public List<StockInViewModel> GetStockBetweenDates(string startDate,string endDate)
        {
            var stockList = new List<StockInViewModel>();
            using (_dbContext = new AccountdbContext())
            {
                stockList = (from s in _dbContext.StockIn
                             where s.CreatedDate >= Convert.ToDateTime(startDate)
                             where s.CreatedDate <= Convert.ToDateTime(endDate)
                             select new StockInViewModel
                             {
                                 id = s.StockId,
                                 loadName = s.LoadName,
                                 vendorId = s.VendorId,
                                 totalQuantity = s.Quantity,
                                 isActive = Convert.ToBoolean(s.IsActive)
                             }).ToList();
            }

            return stockList;
        }

        public bool CheckIsDuplicateNickName([FromBody] string data)
        {
            using (_dbContext = new AccountdbContext())
            {
                if (_dbContext.StockIn.Any(x => x.LoadName.ToLower() == data.ToLower() && x.IsActive == true))
                {
                    return true;
                }
            }

            return false;
        }
        public bool DeleteStockById(int id)
        {
            bool result = false;
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    var stockInfo = _dbContext.StockIn.Where(e => e.StockId.Equals(id)).FirstOrDefault();
                    stockInfo.IsActive = false;

                    _dbContext.StockIn.Update(stockInfo);
                    _dbContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private  EfDbContext.StockIn ConstructStockInAddressAsPerContext(StockInViewModel item)
        {
            return new EfDbContext.StockIn
            {
                VendorId = item.vendorId,
                LoadName = item.loadName,
                Quantity = item.totalQuantity,
                UpdatedDate = DateTime.Today,
                
            };
        }

       

        
    }
}
