using AccountsApiServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountsApiServices.Controllers
{
    public class SalesController : ApiController
    {
        // GET: api/Sales
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sales/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/Sales/AddSales")]
        [HttpPost]
        public string AddSales(List<Sales> data)
        {
            Console.WriteLine(data);
            return "Ok";
        }


        // POST: api/Sales
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Sales/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sales/5
        public void Delete(int id)
        {
        }
    }
}
