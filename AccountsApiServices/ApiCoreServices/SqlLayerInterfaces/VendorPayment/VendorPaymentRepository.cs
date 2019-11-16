using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreServices.EfDbContext;
using ApiCoreServices.Models;
using ApiCoreServices.ServicesModels;
using ApiCoreServices.ViewModels;

namespace ApiCoreServices.SqlLayerInterfaces.VendorPayment
{
    public class VendorPaymentRepository : IVendorPaymentRepository
    {
        AccountdbContext _dbContext;
        public VendorPaymentRepository(AccountdbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CheckIsDuplicateNickName(string data)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVendorById(int id)
        {
            throw new NotImplementedException();
        }

        public List<VendorViewModel> GetAllVendors()
        {
            throw new NotImplementedException();
        }

        public VendorViewModel GetVendorById(int id)
        {
            throw new NotImplementedException();
        }

        
        public CommonResponseViewModel SaveVendorPayment(VendorPaymentViewModel vendorPaymentVM)
        {
            var vendorPayment = ConstructVendorPaymentVModelToContext(vendorPaymentVM);
            AddVendorPaymentToDb(vendorPayment);

            return null;
        }

        private void AddVendorPaymentToDb(VendorPayments vendorPayment)
        {
            using (_dbContext = new AccountdbContext())
            {
                _dbContext.VendorPayments.Add(vendorPayment);
                _dbContext.SaveChanges();
            }
        }

        private EfDbContext.VendorPayments ConstructVendorPaymentVModelToContext(VendorPaymentViewModel vendorPaymentVM)
        {
            return new EfDbContext.VendorPayments
            {
                StockInId = HelperUtility.ConvertLongToInt(vendorPaymentVM.StockInId),
                VendorId = HelperUtility.ConvertLongToInt(vendorPaymentVM.VendorId),
                AmountPaid = vendorPaymentVM.AmountPaid,
            };
        }

        public List<VendorViewModel> VendorNames(AutoCompleteModel data)
        {
            var vendorList = new List<VendorViewModel>();
            try
            {
                var vendors = _dbContext.Vendor.Where(e => e.NickName.Contains(data.q)).ToList();
                if (vendors.Any())
                {
                    foreach (var item in vendors)
                    {
                        vendorList.Add(ConstructVendorViewModelFromContext(item));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vendorList;
        }

        private VendorViewModel ConstructVendorViewModelFromContext(EfDbContext.Vendor item)
        {
            return new VendorViewModel
            {
                firstName = item.FirstName,
                lastName = item.LastName,
                middleName = item.MiddleName,
                id = HelperUtility.ConvertLongToInt(item.VendorId),
                mobile = item.MobileNo,
                nickName = item.NickName,

            };
        }
    }
}
