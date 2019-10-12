using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AccountsApiServices.Models;

namespace AccountsApiServices.Controllers
{

    public class RequestParams
    {
        public string q { get; set; }
        public string sort { get; set; }

        public string order { get; set; }

    }
    public class VendorController : ApiController
    {
        
        // GET api/values
        public Vendor Get()
        {
            return new Vendor { firstName = "rams", lastName = "rams1" };
        }

        [Route("api/Vendor/VendorNames")]
        [HttpPost]
        public List<Vendor> VendorNames(RequestParams data)
        {
            Console.WriteLine(data);

            var objdata = new List<Vendor>()
            {
                new Vendor { firstName = "rams", lastName = "rams11" },
                new Vendor { firstName = "rams1", lastName = "rams12" },

                new Vendor { firstName = "rams2", lastName = "rams13" },

                new Vendor { firstName = "rams3", lastName = "rams13" }



            };

            var obj = objdata.Where(e => e.firstName.Contains(data.q)).ToList();
            return obj;

        }

       

        [HttpPost]
        [Route("api/Vendor/AddVendor")]
        public IHttpActionResult AddVendor(Vendor data)
        {
            //return new string[] { "value1", "value2" };
            // return new Vishnu { name = "rams", password = "rams1" };
           Console.WriteLine(data);
           return Ok(data);

        }

       
       
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
