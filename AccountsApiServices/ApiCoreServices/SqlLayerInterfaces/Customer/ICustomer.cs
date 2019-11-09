using ApiCoreServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreServices.SqlLayerInterfaces
{
    public interface ICustomerRepository
    {
         CommonResponseViewModel SaveCustomer(CustomerViewModel customerVM);
    }
}
