using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountsApiServices.Controllers
{
    public class StockInController : ApiController
    {
        // GET: api/StockIn
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/StockIn/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/StockIn
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/StockIn/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StockIn/5
        public void Delete(int id)
        {
        }
    }
}
