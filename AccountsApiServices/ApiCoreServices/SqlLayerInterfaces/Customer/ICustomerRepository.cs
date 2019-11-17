using ApiCoreServices.Models;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreServices.SqlLayerInterfaces
{
    public interface ICustomerRepository
    {
        CommonResponseViewModel AddCustomer(CustomerViewModel customerVM);

        CommonResponseViewModel UpdateCustomer(CustomerViewModel customerVM);

        CustomerViewModel GetCustomerById(int id);

        List<CustomerViewModel> CustomerNames(AutoCompleteModel data);

        List<CustomerViewModel> GetAllCustomers();

        bool CheckIsDuplicateNickName([FromBody]string data);

        bool DeleteCustomerById(int id);
    }
}
