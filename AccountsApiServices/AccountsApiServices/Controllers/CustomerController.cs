using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AccountsApiServices.Models;

namespace AccountsApiServices.Controllers
{
    public class CustomerController : ApiController
    {
        // GET api/values
        public Customer Get()
        {
            //return new string[] { "value1", "value2" };
            return new Customer { firstName = "rams", lastName = "rams1" };

        }

        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST: api/Customer
        //public void Post([FromBody]string value)
        //{
        //}

        [HttpPost]
        [Route("api/Customer/AddCustomer")]
        public IHttpActionResult AddCustomer(Customer data)
        {
            //return new string[] { "value1", "value2" };
            // return new Vishnu { name = "rams", password = "rams1" };
            Console.WriteLine(data);
            return Ok(data);

        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
