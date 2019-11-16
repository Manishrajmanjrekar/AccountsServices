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
    public class SalesRepository : ISalesRepository
    {
        AccountdbContext _dbContext;
        public SalesRepository(AccountdbContext dbContext)
        {
              _dbContext = dbContext;
        }

        public List<SalesViewModel> LoadCustNames(AutoCompleteModel data)
        {
            var SalesList = new List<SalesViewModel>();
            try
            {

                SalesList = (from c in _dbContext.Customer
                 where c.NickName.Contains(data.q)
                 select new SalesViewModel
                 {
                     CustomerId = HelperUtility.ConvertLongToInt(c.CustId),
                     CustomerName = HelperUtility.GetCustName(c)

                 }).ToList();



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SalesList;
        }

        public List<SalesViewModel> LoadVendorNames(AutoCompleteModel data)
        {
            var SalesList = new List<SalesViewModel>();
            try
            {
                SalesList = (from v in _dbContext.Vendor
                             where v.NickName.Contains(data.q)
                             select new SalesViewModel
                             {
                                 VendorId = HelperUtility.ConvertLongToInt(v.VendorId),
                                 VendorName = HelperUtility.GetVendorName(v)

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SalesList;
        }

        public SalesViewModel AddSales(SalesViewModel data)
        {
            try
            {
                _dbContext.Sales.Add(ConstructSalesAsPerContext(data));
                _dbContext.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }

            return data;

        }

        public SalesViewModel UpdateSales(SalesViewModel data)
        {
            try
            {
                var dbrecord = _dbContext.Sales.Where(e => e.SalesId.Equals(data.Id)).FirstOrDefault();
                dbrecord.IsActive = false;

                //updating the old record with IsActive False;
                _dbContext.Sales.Update(dbrecord);

                //Adding new record with updated values sales record..................................
                 data.Id = 0;
                 var newRecord = ConstructSalesAsPerContext(data);
                _dbContext.Sales.Add(newRecord);
                _dbContext.SaveChanges();

                //return the new record to the UI......
                data =  ConstructSalesViewModelFromPerContext(newRecord);
            }
            catch (Exception)
            {

                throw;
            }

            return data;

        }

        public bool AddMultipleSales(List<SalesViewModel> data)
        {
            try
            {
                if (data.Any())
                {
                    foreach (var item in data)
                    {
                        _dbContext.Sales.Add(ConstructSalesAsPerContext(item));
                        _dbContext.SaveChanges();

                    }
                }
               
                return true;

            }
            catch (Exception)
            {

                throw;
            }

            return false;

        }

        public List<SalesViewModel> GetSalesBySalesId(string id)
        {
            var salesid = Convert.ToInt64(id);
            var SalesList = new List<SalesViewModel>();
            try
            {

                SalesList = (from stock in _dbContext.StockIn
                             from sales in _dbContext.Sales
                             from vendor in _dbContext.Vendor
                             from cust in _dbContext.Customer
                             where sales.SalesId.Equals(salesid)
                             && stock.StockId.Equals(sales.StockIn) &&
                             stock.VendorId.Equals(vendor.VendorId) &&
                             sales.CustId.Equals(cust.CustId)
                             select new SalesViewModel
                             {
                                 VendorId = HelperUtility.ConvertLongToInt(vendor.VendorId),
                                 StockInId = HelperUtility.ConvertLongToInt(stock.StockId),
                                 CustomerId = HelperUtility.ConvertLongToInt(cust.CustId),
                                 CustomerName = HelperUtility.GetCustName(cust),
                                 LoadName = stock.LoadName,
                                 Id = HelperUtility.ConvertLongToInt(sales.SalesId),
                                 Quantity = sales.Quantity,
                                 Total = sales.Total,
                                 Price = sales.Price,
                                 CreatedDate = sales.CreatedDate.ToString(),
                                 VendorName = HelperUtility.GetVendorName(vendor),

                             }).ToList();



            }
            catch (Exception)
            {

            }
            return SalesList;
        }

        public List<SalesViewModel> GetSalesByStockInId(string id)
        {
            var stockInid = Convert.ToInt64(id);
            var SalesList = new List<SalesViewModel>();
            try
            {

                SalesList = (from stock in _dbContext.StockIn
                             from sales in _dbContext.Sales
                             from vendor in _dbContext.Vendor
                             from cust in _dbContext.Customer
                             where stock.StockId.Equals(stockInid)
                             && stock.StockId.Equals(sales.StockIn) &&
                             stock.VendorId.Equals(vendor.VendorId) &&
                             sales.CustId.Equals(cust.CustId)
                             select new SalesViewModel
                             {
                                 VendorId = HelperUtility.ConvertLongToInt(vendor.VendorId),
                                 StockInId = HelperUtility.ConvertLongToInt(stock.StockId),
                                 CustomerId = HelperUtility.ConvertLongToInt(cust.CustId),
                                 CustomerName = HelperUtility.GetCustName(cust),
                                 LoadName = stock.LoadName,
                                 Id = HelperUtility.ConvertLongToInt(sales.SalesId),
                                 Quantity = sales.Quantity,
                                 Total = sales.Total,
                                 Price = sales.Price,
                                 CreatedDate = sales.CreatedDate.ToString(),
                                 VendorName = HelperUtility.GetVendorName(vendor),

                             }).ToList();



            }
            catch (Exception)
            {

            }
            return SalesList;
        }

        public List<SalesViewModel> GetSalesByVendorId(string id)
        {
            var vendorId = Convert.ToInt64(id);
            var SalesList = new List<SalesViewModel>();
            try
            {

                SalesList = (from stock in _dbContext.StockIn
                             from sales in _dbContext.Sales
                             from vendor in _dbContext.Vendor
                             from cust in _dbContext.Customer
                             where vendor.VendorId.Equals(vendorId)
                             && stock.StockId.Equals(sales.StockIn) &&
                             stock.VendorId.Equals(vendor.VendorId) &&
                             sales.CustId.Equals(cust.CustId)
                             select new SalesViewModel
                             {
                                 VendorId = HelperUtility.ConvertLongToInt(vendor.VendorId),
                                 StockInId = HelperUtility.ConvertLongToInt(stock.StockId),
                                 CustomerId = HelperUtility.ConvertLongToInt(cust.CustId),
                                 CustomerName = HelperUtility.GetCustName(cust),
                                 LoadName = stock.LoadName,
                                 Id = HelperUtility.ConvertLongToInt(sales.SalesId),
                                 Quantity = sales.Quantity,
                                 Total = sales.Total,
                                 Price = sales.Price,
                                 CreatedDate = sales.CreatedDate.ToString(),
                                 VendorName = HelperUtility.GetVendorName(vendor),

                             }).ToList();



            }
            catch (Exception)
            {

            }
            return SalesList;
        }

        public List<SalesViewModel> GetSalesByCustomerId(string id)
        {
            var customerId = Convert.ToInt64(id);
            var SalesList = new List<SalesViewModel>();
            try
            {

                SalesList = (from stock in _dbContext.StockIn
                             from sales in _dbContext.Sales
                             from vendor in _dbContext.Vendor
                             from cust in _dbContext.Customer
                             where sales.CustId.Equals(customerId)
                             && stock.StockId.Equals(sales.StockIn) &&
                             stock.VendorId.Equals(vendor.VendorId) &&
                             sales.CustId.Equals(cust.CustId)
                             select new SalesViewModel
                             {
                                 VendorId = HelperUtility.ConvertLongToInt(vendor.VendorId),
                                 StockInId = HelperUtility.ConvertLongToInt(stock.StockId),
                                 CustomerId = HelperUtility.ConvertLongToInt(cust.CustId),
                                 CustomerName = HelperUtility.GetCustName(cust),
                                 LoadName = stock.LoadName,
                                 Id = HelperUtility.ConvertLongToInt(sales.SalesId),
                                 Quantity = sales.Quantity,
                                 Total = sales.Total,
                                 Price = sales.Price,
                                 CreatedDate = sales.CreatedDate.ToString(),
                                 VendorName = HelperUtility.GetVendorName(vendor),

                             }).ToList();



            }
            catch (Exception)
            {

            }
            return SalesList;
        }

        public int TotalStockSold(string id)
        {
            var stockId = Convert.ToInt64(id);
            var countOfRecoreds =_dbContext.Sales
                .Where(e => e.StockIn.Equals(stockId))
                .Select(p=>p).Count();
            return countOfRecoreds;
        }

        public List<SalesViewModel> GetSalesBetweenDates(string startDate, string endDate)
        {
            var SalesList = new List<SalesViewModel>();

            try
            {
                SalesList = (from stock in _dbContext.StockIn
                             from sales in _dbContext.Sales
                             from vendor in _dbContext.Vendor
                             from cust in _dbContext.Customer
                             where sales.CreatedDate >= Convert.ToDateTime(startDate)
                             where sales.CreatedDate <= Convert.ToDateTime(endDate)
                             && stock.StockId.Equals(sales.StockIn) &&
                             stock.VendorId.Equals(vendor.VendorId) &&
                             sales.CustId.Equals(cust.CustId)
                             select new SalesViewModel
                             {
                                 VendorId = HelperUtility.ConvertLongToInt(vendor.VendorId),
                                 StockInId = HelperUtility.ConvertLongToInt(stock.StockId),
                                 CustomerId = HelperUtility.ConvertLongToInt(cust.CustId),
                                 CustomerName = HelperUtility.GetCustName(cust),
                                 LoadName = stock.LoadName,
                                 Id = HelperUtility.ConvertLongToInt(sales.SalesId),
                                 Quantity = sales.Quantity,
                                 Total = sales.Total,
                                 Price = sales.Price,
                                 CreatedDate = sales.CreatedDate.ToString(),
                                 VendorName = HelperUtility.GetVendorName(vendor),

                             }).ToList();

            }
            catch (Exception)
            {

                throw;
            }
            return SalesList;
        }

        
        private  EfDbContext.Sales ConstructSalesAsPerContext(SalesViewModel item)
        {
            return new EfDbContext.Sales
            {
                CustId = HelperUtility.ConvertLongToInt(item.CustomerId),
                StockInId = HelperUtility.ConvertLongToInt(item.StockInId),
                Price = item.Price,
                Quantity = item.Quantity,
                Total = item.Total,

            };
        }

        private SalesViewModel ConstructSalesViewModelFromPerContext(EfDbContext.Sales item)
        {
            return new SalesViewModel
            {
                CustomerId = HelperUtility.ConvertLongToInt(item.CustId.Value),
                StockInId = HelperUtility.ConvertLongToInt(item.StockInId.Value),
                Price = item.Price,
                Quantity = item.Quantity,
                Total = item.Total,
                CustomerName = HelperUtility.GetCustName(item.Cust),
                VendorName = HelperUtility.GetVendorName(item.StockIn.Vendor),
                VendorId = HelperUtility.ConvertLongToInt(item.StockIn.VendorId)


            };
        }




    }
}
