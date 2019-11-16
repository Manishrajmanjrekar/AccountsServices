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
    public class CustomerRepository : ICustomerRepository
    {
        AccountdbContext _dbContext;
        public CustomerRepository(AccountdbContext dbContext)
        {
              _dbContext = dbContext;
        }

        public List<CustomerViewModel> CustomerNames(AutoCompleteModel data)
        {
            var custList = new List<CustomerViewModel>();
            try
            {
                var customerList = new List<EfDbContext.Customer>();
                using (_dbContext= new AccountdbContext())
                {
                    customerList = _dbContext.Customer.Where(e => e.NickName.Contains(data.q)).ToList();

                }

                    if (customerList.Any())
                    {
                        foreach (var item in customerList)
                        {
                            custList.Add(ConstructCustomerViewModelFromContext(item));
                        }
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return custList;
        }

        public CustomerViewModel GetCustomerById(int id)
        {
            var customerViewModel = new CustomerViewModel();
            var cust = new EfDbContext.Customer();
            try
            {
               
                using (_dbContext = new AccountdbContext())
                {
                    cust = _dbContext.Customer.Where(e => e.CustId.Equals(id)).FirstOrDefault();
                }
                customerViewModel = ConstructCustomerViewModelFromContext(cust);
            }
            catch (Exception)
            {

                throw;
            }

            return customerViewModel;
        }

        public CommonResponseViewModel SaveCustomer(CustomerViewModel customerVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            try
            {
                var cust = new EfDbContext.Customer();
                cust.FirstName = customerVM.firstName;
                cust.MiddleName = customerVM.middleName;
                cust.NickName = customerVM.nickName;
                cust.LastName = customerVM.lastName;
                cust.Mobile = customerVM.mobile;
                cust.ReferredBy = customerVM.referredBy;
                cust.CreatedBy = customerVM.createdBy;
                cust.ModifiedBy = customerVM.modifiedBy;

                using (_dbContext = new AccountdbContext())
                {
                    //Save the customer entity first........
                    _dbContext.Customer.Add(cust);
                    _dbContext.SaveChanges();

                    response.isSuccess = true;
                    //update the custid from db to viewModel............................
                    response.recordId = HelperUtility.ConvertLongToInt(cust.CustId);
                    customerVM.id = response.recordId;

                    //Loop all the address list and update the address in the db....................
                    if (customerVM.CustAddressList.Any())
                    {
                        foreach (var item in customerVM.CustAddressList)
                        {
                            _dbContext.CustomerDetails.Add(ConstructCustAddressAsPerContext(item, customerVM.id));
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            

            return response;
        }

        public List<CustomerViewModel> GetAllCustomers()
        {
            var custList = new List<CustomerViewModel>();
            var dbcustList = new List<EfDbContext.Customer>();
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    dbcustList = _dbContext.Customer.ToList();
                }
                if (dbcustList.Any())
                {
                    foreach (var item in dbcustList)
                    {
                        custList.Add(ConstructCustomerViewModelFromContext(item));
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            
            return custList;

        }

        public bool CheckIsDuplicateNickName([FromBody] string data)
        {
            bool results = false;
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    results = _dbContext.Customer.Any(x => x.NickName.ToLower() == data.ToLower()
                               && x.IsActive == true);

                }
            }
            catch (Exception)
            {

                throw;
            }
            

            return results;
        }
        public bool DeleteCustomerById(int id)
        {
            bool results = false;
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    var cust = _dbContext.Customer.Where(e => e.CustId.Equals(id)).FirstOrDefault();
                    cust.IsActive = false;
                    _dbContext.Customer.Update(cust);
                    _dbContext.SaveChanges();
                    results = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
          
           
            return false;

        }

        private  EfDbContext.CustomerDetails ConstructCustAddressAsPerContext(ViewModels.CustomerDetails item,int custId)
        {
            return new EfDbContext.CustomerDetails
            {
                CustId = custId,
                Address = item.address,
                AlternateMobile = item.alternateMobile,
                City = item.city,
                State = item.state,
                HomePhone = item.homePhone,
                OfficePhone = item.officePhone,
                ShopLocation = item.shopLocation,
                ShopName = item.shopLocation,
                Email = item.email
            };
        }

        private CustomerViewModel ConstructCustomerViewModelFromContext(EfDbContext.Customer item)
        {
            return new CustomerViewModel
            {
                firstName = item.FirstName,
                lastName = item.LastName,
                middleName = item.MiddleName,
                id = HelperUtility.ConvertLongToInt(item.CustId),
                mobile = item.Mobile
            };
        }

        
    }
}
