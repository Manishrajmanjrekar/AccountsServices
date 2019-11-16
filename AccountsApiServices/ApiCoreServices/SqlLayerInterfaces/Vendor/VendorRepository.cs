using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreServices.EfDbContext;
using ApiCoreServices.Models;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreServices.SqlLayerInterfaces.Customer
{
    public class VendorRepository : IVendorRepository
    {
        AccountdbContext _dbContext;
        public VendorRepository(AccountdbContext dbContext)
        {
              _dbContext = dbContext;
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

        public VendorViewModel GetVendorById(int id)
        {
            var vendorViewModel = new VendorViewModel();

            try
            {
                var vendor = _dbContext.Vendor.Where(e => e.VendorId.Equals(id)).FirstOrDefault();
                vendorViewModel = ConstructVendorViewModelFromContext(vendor);
            }
            catch (Exception)
            {

                throw;
            }

            return vendorViewModel;
        }

        public CommonResponseViewModel SaveVendor(VendorViewModel vendorVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            var vendor = new EfDbContext.Vendor();
            vendor.FirstName = vendorVM.firstName;
            vendor.MiddleName = vendorVM.middleName;
            vendor.NickName = vendorVM.nickName;
            vendor.LastName = vendorVM.lastName;
            vendor.MobileNo = vendorVM.mobile;
            vendor.ReferredBy = vendorVM.referredBy;
            vendor.CreatedBy = vendorVM.createdBy;
            vendor.ModifiedBy = vendorVM.modifiedBy;

            using (_dbContext = new AccountdbContext())
            {
                //Save the customer entity first........
                _dbContext.Vendor.Add(vendor);
                _dbContext.SaveChanges();

                response.isSuccess = true;
                //update the custid from db to viewModel............................
                response.recordId = HelperUtility.ConvertLongToInt(vendor.VendorId);
                vendorVM.id = response.recordId;

                //Loop all the address list and update the address in the db....................
                if (vendorVM.VendorAddressList.Any())
                {
                    foreach (var item in vendorVM.VendorAddressList)
                    {
                        _dbContext.VendorDetails.Add(ConstructVendorAddressAsPerContext(item, vendorVM.id));
                    }
                }
            }

            return response;
        }

        public List<VendorViewModel> GetAllVendors()
        {
            var vendorList = new List<VendorViewModel>();

            var dbvendortList = _dbContext.Vendor.ToList();
            if (vendorList.Any())
            {
                foreach (var item in dbvendortList)
                {
                    vendorList.Add(ConstructVendorViewModelFromContext(item));
                }
            }

            return vendorList;

        }

        public bool CheckIsDuplicateNickName([FromBody] string data)
        {
            if (_dbContext.Vendor.Any(x => x.NickName.ToLower() == data.ToLower() && x.IsActive == true))
            {
                return true;
            }

            return false;
        }
        public bool DeleteVendorById(int id)
        {
            var vendor = _dbContext.Vendor.Where(e => e.VendorId.Equals(id)).FirstOrDefault();
            vendor.IsActive = false;

            _dbContext.Vendor.Update(vendor);
            _dbContext.SaveChanges();
            return true;

        }

        private  EfDbContext.VendorDetails ConstructVendorAddressAsPerContext(ViewModels.VendorDetails item,int vendorId)
        {
            return new EfDbContext.VendorDetails
            {
                VendorId = vendorId,
                Address = item.address,
                AlternateMobile = item.alternateMobile,
                City = item.city,
                HomePhone =item.homePhone,
                State = item.state,
                Email = item.email
            };
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
