using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public CommonResponseViewModel SaveCustomer(CustomerViewModel CustomerVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            List<CustomerViewModel> Customers = GetCustomers();

            if (CustomerVM.id == 0)
            {
                // insert
                CustomerVM.id = Customers[Customers.Count - 1].id + 1;
                //CustomerVM.Url = "/Customer/" + CustomerVM.id;
                Customers.Add(CustomerVM);

                response.isSuccess = true;
                response.recordId = CustomerVM.id;
            }
            else
            {
                // update
                CustomerViewModel Customer = Customers.Where(x => x.id == CustomerVM.id).FirstOrDefault();

                if (Customer != null)
                {
                    Customer.firstName = CustomerVM.firstName;
                    Customer.address = CustomerVM.address;
                    Customer.city = CustomerVM.city;
                    Customer.referredBy = CustomerVM.referredBy;
                    Customer.mobile = CustomerVM.mobile;

                    response.isSuccess = true;
                    response.recordId = CustomerVM.id;
                }
                else // TO DO: remove this temp else block
                {
                    CustomerVM.id = Customers[Customers.Count - 1].id + 1;
                    //CustomerVM.Url = "/Customer/" + CustomerVM.id;
                    Customers.Add(CustomerVM);

                    response.isSuccess = true;
                    response.recordId = CustomerVM.id;
                }
            }

            //if (response.IsSuccess)
            //{
            //    _cache.Set("Customers", Customers);
            //}

            return response;
        }

        [HttpDelete]
        [Route("api/Customer/Delete/{id}")]
        public bool DeleteCustomer(int id)
        {
            bool isSuccess = false;
            List<CustomerViewModel> Customers = GetCustomers();
            if (id > 0 && Customers != null)
            {
                Customers = Customers.Where(x => x.id != id).ToList();
                //_cache.Set("Customers", Customers);

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

            List<string> exitingNickNames = new List<string>()
            {
                "ramesh",
                "suresh",
                "ajay",
                "vijay",
            };

            if (exitingNickNames.Contains(data, StringComparer.OrdinalIgnoreCase))
            {
                isDuplicateNickName = true;
            }

            return isDuplicateNickName;
        }

        private List<CustomerViewModel> GetCustomers()
        {
            List<CustomerViewModel> Customers = null;
            //_cache.TryGetValue("Customers", out Customers);

            if (Customers == null)
            {
                Customers = new List<CustomerViewModel>
                {
                    new CustomerViewModel { id = 1, firstName = "Arjun", address="Dilshuknagar", city="Hyderabad", referredBy="Referrer 1", mobile = "9923456789"},
                    new CustomerViewModel { id = 2, firstName = "Rizwan", address="Nagole", city="Hyderabad", referredBy="Referrer 1", mobile = "8823456789"},
                    new CustomerViewModel {
                         id = 3,
                         nickName = "AjayTej",
                         firstName = "Ajay",
                         middleName = "mn",
                         lastName = "Teja",
                         mobile = "9999999999",
                         alternateMobile = "8888888888",
                         homePhone = "8666666666",
                         officePhone = "8555555555",
                         email = "Ajay@abc.com",
                         address = "Ajay address",
                         city = "Ajay city",
                         state = "Ajay state",
                         shopName = "Ajay shop",
                         shopLocation = "Ajay shop ",
                         referredBy = "Ajay referrer",
                    }
                };

                //Customers.ForEach(x => x.Url = "/Customer/" + x.CustomerId);


                //_cache.Set("Customers", Customers);
            }

            return Customers;
        }

    }
}
