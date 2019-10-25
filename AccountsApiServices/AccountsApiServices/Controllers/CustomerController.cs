using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AccountsApiServices.Models;
using AccountsApiServices.ViewModels;

namespace AccountsApiServices.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpPost]
        [Route("api/Customer/AddCustomer")]
        public IHttpActionResult AddCustomer(Customer data)
        {
            //return new string[] { "value1", "value2" };
            // return new Vishnu { name = "rams", password = "rams1" };
            Console.WriteLine(data);
            return Ok(data);

        }

        [HttpGet]
        [Route("api/Customer")]
        public List<CustomerViewModel> Get()
        {
            return GetCustomers();
        }

        [HttpGet]
        [Route("api/Customer/{id}")]
        public CustomerViewModel Get(int id)
        {
            CustomerViewModel Customer = null;

            List<CustomerViewModel> Customers = GetCustomers();
            Customer = Customers.Where(x => x.id == id).FirstOrDefault();

            return Customer;
        }

        [HttpPost]
        [Route("api/Customer/SaveCustomer")]
        public CommonResponseViewModel SaveCustomer(CustomerViewModel customerVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            List<CustomerViewModel> customers = GetCustomers();

            if (customerVM.id <= 0)
            {
                // insert
                customerVM.id = customers[customers.Count - 1].id + 1;
                customers.Add(customerVM);

                response.isSuccess = true;
                response.recordId = customerVM.id;
            }
            else
            {
                // update
                var customer = customers.Where(x => x.id == customerVM.id).FirstOrDefault();
                if (customer != null)
                {
                    int index = customers.FindIndex(x => x.id == customerVM.id);
                    customers[index] = customer;

                    response.isSuccess = true;
                    response.recordId = customerVM.id;
                }
            }

            if (response.isSuccess)
            {
                HttpContext.Current.Cache.Remove("Customers");
                HttpContext.Current.Cache.Insert("Customers", customers);
            }

            return response;
        }

        [HttpDelete]
        [Route("api/Customer/Delete/{id}")]
        public bool DeleteCustomer(int id)
        {
            bool isSuccess = false;
            List<CustomerViewModel> customers = GetCustomers();
            if (id > 0 && customers != null)
            {
                customers = customers.Where(x => x.id != id).ToList();
                HttpContext.Current.Cache.Remove("Customers");
                HttpContext.Current.Cache.Insert("Customers", customers);

                isSuccess = true;
            }

            return isSuccess;
        }

        [Route("api/Customer/CheckIsDuplicateNickName")]
        [HttpPost]
        public bool CheckIsDuplicateNickName([FromBody]string data)
        {
            bool isDuplicateNickName = false;

            if (string.IsNullOrWhiteSpace(data))
                return isDuplicateNickName;

            var customers = GetCustomers();
            if (customers != null && customers.Any(x => x.nickName.ToLower() == data.ToLower()))
            {
                isDuplicateNickName = true;
            }

            return isDuplicateNickName;
        }
     
        private List<CustomerViewModel> GetCustomers()
        {
            List<CustomerViewModel> customers = null;
            if (HttpContext.Current.Cache.Get("Customers") != null)
            {
                customers = (List<CustomerViewModel>)HttpContext.Current.Cache.Get("Customers");
            }

            if (customers == null || customers.Count == 0)
            {
                List<string> addresses = new List<string>()
                { "Ameerpet", "Kukatpally", "Mehdipatnam", "Madhapur", "Hitech City", "Dilsukhnagar", "Uppal" };

                List<string> customerNames = new List<string>()
                {
                    "Aditya","Akshat","Anubhav","Arjun","Ashish","Bhaskar","Bikram","Chetas","Chiranjeev","Daljeet","Dilip",
                    "Gaurav","Gautam","Girish","Gurdeep","Indiresh","Ishranth","Jagan","Jaideep","Jeet","Karun","Lalit",
                    "Manvik","Mridul","Naagesh","Naishadh","Nilaksh","Parth","Pavan","Pranav",
                    "Rajiv","Ritesh","Sachin","Samir","Sanchit","Sandeep","Sanjay","Siddharth","Sparsh",
                    "Tarun","Tushar","Udit","Uttam","Varun","Vikas","Vinay","Vipul","Yashwant"
                };

                List<string> lastNames = new List<string>()
                {
                    "Sharma","Verma","Gupta","Malhotra","Bhatnagar","Saxena","Kapoor","Singh","Mehra","Chopra","Sarin",
                    "Dutt","Rao","Singh","Yadav","Jhadav","Jaiteley","Chauhan","Khan"
                };


                Random random = new Random();

                customers = new List<CustomerViewModel>();
                CustomerViewModel customer = new CustomerViewModel();

                for (int recordId = 1; recordId <= 100; recordId++)
                {
                    string name = customerNames[random.Next(1, customerNames.Count())];

                    customer = new CustomerViewModel();
                    customer.id = recordId;
                    customer.nickName = name + recordId;
                    customer.firstName = name;
                    customer.middleName = "";
                    customer.lastName = lastNames[random.Next(1, lastNames.Count())];
                    customer.mobile = ("999999999" + recordId);
                    customer.alternateMobile = ("888888888" + recordId);
                    customer.homePhone = ("777777777" + recordId);
                    customer.email = name + "@abc.com";
                    customer.address = addresses[random.Next(1, addresses.Count())];
                    customer.city = "Hyderabad";
                    customer.state = "Telangana";
                    customer.referredBy = "Referrer " + random.Next(1, 10).ToString();

                    customer.mobile = customer.mobile.Length > 10 ? customer.mobile.Substring(customer.mobile.Length - 10, 10) : customer.mobile;
                    customer.alternateMobile = customer.alternateMobile.Length > 10 ? customer.alternateMobile.Substring(customer.alternateMobile.Length - 10, 10) : customer.alternateMobile;
                    customer.homePhone = customer.homePhone.Length > 10 ? customer.homePhone.Substring(customer.homePhone.Length - 10, 10) : customer.homePhone;

                    customer.shopName = name + " shop";
                    customer.shopLocation = addresses[random.Next(1, addresses.Count())];
                    customer.officePhone = ("666666666" + recordId);
                    customer.officePhone = customer.officePhone.Length > 10 ? customer.officePhone.Substring(customer.officePhone.Length - 10, 10) : customer.officePhone;

                    customers.Add(customer);
                };

                HttpContext.Current.Cache.Insert("Customers", customers);
            }

            return customers;
        }

    }
}
