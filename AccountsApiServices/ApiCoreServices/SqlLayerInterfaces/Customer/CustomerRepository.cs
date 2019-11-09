using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreServices.EfDbContext;
using ApiCoreServices.ViewModels;

namespace ApiCoreServices.SqlLayerInterfaces.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        AccountdbContext _dbContext;
        public CustomerRepository(AccountdbContext dbContext)
        {
              _dbContext = dbContext;
        }

        public CommonResponseViewModel SaveCustomer(CustomerViewModel customerVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            var cust = new EfDbContext.Customer();
            cust.FirstName = customerVM.firstName;
            cust.MiddleName = customerVM.middleName;
           // cust.FirstName = customerVM.firstName;

            _dbContext.Customer.Add(cust);
            _dbContext.SaveChanges();

            response.isSuccess = true;
            response.recordId = customerVM.id;
            return response;
        }
    }
}
