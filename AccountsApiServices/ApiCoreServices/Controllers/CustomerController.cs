using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreServices.Models;
using ApiCoreServices.SqlLayerInterfaces;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerRepository _CustomerRepository;


        public CustomerController(ICustomerRepository CustomerRepository)
        {
            _CustomerRepository = CustomerRepository;
        }
        // GET: api/Customer
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}



        [HttpPost]
        //[EnableCors("AllowAll")]
        [Route("PostCustomer")]
        public CommonResponseViewModel PostCustomer(CustomerViewModel customerVM)
        {
            var response = _CustomerRepository.SaveCustomer(customerVM);
            Console.WriteLine();
            return response;
        }

        [HttpPost]
        [Route("UpdateCustomer")]
        public CommonResponseViewModel UpdateCustomer(CustomerViewModel customerVM)
        {
            var response = _CustomerRepository.UpdateCustomer(customerVM);
            Console.WriteLine();
            return response;
        }

        [HttpPost]
        [Route("GetCustomerById")]
        public CustomerViewModel GetCustomerById(int id)
        {
            CustomerViewModel Customer = null;
            Customer = _CustomerRepository.GetCustomerById(id);
            return Customer;
        }

        [Route("CustomerNames")]
        [HttpPost]
        public List<CustomerViewModel> CustomerNames(AutoCompleteModel data)
        {
            return _CustomerRepository.CustomerNames(data);
        }



        [HttpPost]
        [Route("GetAllCustomers")]
        public List<CustomerViewModel> GetAllCustomers()
        {
            return _CustomerRepository.GetAllCustomers();
        }

        [Route("CheckIsDuplicateNickName")]
        [HttpPost]
        public bool CheckIsDuplicateNickName([FromBody]string data)
        {
            bool isDuplicateNickName = false;

            if (string.IsNullOrWhiteSpace(data))
            {
                throw new Exception("name cannot be empty exception");
            }
           
            if ((_CustomerRepository.CheckIsDuplicateNickName(data)))
            {
                isDuplicateNickName = true;
            }

            return isDuplicateNickName;
        }

        [HttpPost]
        [Route("DeleteCustomerById")]
        public bool DeleteCustomerById(int id)
        {
           var results = _CustomerRepository.DeleteCustomerById(id);
           return results;
        }


    }
}
