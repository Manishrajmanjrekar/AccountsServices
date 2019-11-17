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
        static string _dateFormat = "dd MMM yyyy";

        public VendorRepository(AccountdbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<VendorViewModel> VendorNames(AutoCompleteModel data)
        {
            var vendorList = new List<VendorViewModel>();
            try
            {
                var vendors = new List<EfDbContext.Vendor>();
                using (_dbContext = new AccountdbContext())
                {
                    vendors = _dbContext.Vendor.Where(e => e.NickName.Contains(data.q)).ToList();
                }

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
            var cust = new EfDbContext.Vendor();
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    vendorViewModel = (from v in _dbContext.Vendor
                                       join vd in _dbContext.VendorDetails on v.VendorId equals vd.VendorId
                                       where v.VendorId == id
                                       select ConstructVendorViewModelFromContext(v, vd)
                                      ).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                //Save the vendor entity first........
                _dbContext.Vendor.Add(vendor);
                _dbContext.SaveChanges();

                response.isSuccess = true;
                //update the vendorid from db to viewModel............................
                response.recordId = HelperUtility.ConvertLongToInt(vendor.VendorId);
                vendorVM.id = response.recordId;

                EfDbContext.VendorDetails vendorDetails = new EfDbContext.VendorDetails()
                {
                    VendorId = vendorVM.id,
                    AlternateMobile = vendorVM.alternateMobile,
                    HomePhone = vendorVM.homePhone,
                    Email = vendorVM.email,
                    Address = vendorVM.address,
                    City = vendorVM.city,
                    State = vendorVM.state
                };

                _dbContext.VendorDetails.Add(vendorDetails);
                _dbContext.SaveChanges();

                ////Loop all the address list and update the address in the db....................
                //if (vendorVM.VendorAddressList.Any())
                //{
                //    foreach (var item in vendorVM.VendorAddressList)
                //    {
                //        _dbContext.VendorDetails.Add(ConstructVendorAddressAsPerContext(item, vendorVM.id));
                //    }
                //}
            }

            return response;
        }

        public CommonResponseViewModel UpdateVendor(VendorViewModel vendorVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    var vendor = _dbContext.Vendor.Where(v => v.VendorId.Equals(vendorVM.id)).FirstOrDefault();
                    if (vendor != null)
                    {
                        vendor.FirstName = vendorVM.firstName;
                        vendor.MiddleName = vendorVM.middleName;
                        //vendor.NickName = vendorVM.nickName;
                        vendor.LastName = vendorVM.lastName;
                        vendor.MobileNo = vendorVM.mobile;
                        vendor.ReferredBy = vendorVM.referredBy;
                        vendor.CreatedBy = vendorVM.createdBy;
                        vendor.ModifiedBy = vendorVM.modifiedBy;

                        _dbContext.Update(vendor);

                        var vendorDetails = _dbContext.VendorDetails.Where(c => c.VendorId.Equals(vendorVM.id)).FirstOrDefault();
                        if (vendorDetails != null)
                        {
                            vendorDetails.VendorId = vendorVM.id;
                            vendorDetails.AlternateMobile = vendorVM.alternateMobile;
                            vendorDetails.HomePhone = vendorVM.homePhone;
                            vendorDetails.Email = vendorVM.email;
                            vendorDetails.Address = vendorVM.address;
                            vendorDetails.City = vendorVM.city;
                            vendorDetails.State = vendorVM.state;

                            _dbContext.Update(vendorDetails);
                        }

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

        public List<VendorViewModel> GetAllVendors()
        {
            var vendorList = new List<VendorViewModel>();
            var dbVendorList = new List<EfDbContext.Vendor>();
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    vendorList = (from c in _dbContext.Vendor
                                  join cd in _dbContext.VendorDetails on c.VendorId equals cd.VendorId
                                  select ConstructVendorViewModelFromContext(c, cd)
                                 ).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vendorList;
        }

        public bool CheckIsDuplicateNickName(string data)
        {
            bool results = false;
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    results = _dbContext.Vendor.Any(x => x.NickName.ToLower() == data.ToLower()
                               && x.IsActive == true);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return results;
        }
        public bool DeleteVendorById(int id)
        {
            var vendor = _dbContext.Vendor.Where(e => e.VendorId.Equals(id)).FirstOrDefault();
            vendor.IsActive = false;

            _dbContext.Vendor.Update(vendor);
            _dbContext.SaveChanges();
            return true;

        }

        private EfDbContext.VendorDetails ConstructVendorAddressAsPerContext(ViewModels.VendorDetails item, int vendorId)
        {
            return new EfDbContext.VendorDetails
            {
                VendorId = vendorId,
                Address = item.address,
                AlternateMobile = item.alternateMobile,
                City = item.city,
                HomePhone = item.homePhone,
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

        private static VendorViewModel ConstructVendorViewModelFromContext(EfDbContext.Vendor v, EfDbContext.VendorDetails vd)
        {
            return new VendorViewModel()
            {
                id = v.VendorId,
                nickName = v.NickName,
                firstName = v.FirstName,
                middleName = v.MiddleName,
                lastName = v.LastName,
                mobile = v.MobileNo,
                referredBy = v.ReferredBy,
                createdDate = v.CreatedDate,
                formattedCreatedDate = v.CreatedDate.ToString(_dateFormat),
                createdBy = v.CreatedBy,
                modifiedDate = v.ModifiedDate,
                formattedModifiedDate = v.ModifiedDate.ToString(_dateFormat),
                modifiedBy = v.ModifiedBy,

                alternateMobile = vd.AlternateMobile,
                homePhone = vd.HomePhone,
                email = vd.Email,
                address = vd.Address,
                city = vd.City,
                state = vd.State
            };
        }

    }
}
