using AccountsApiServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountsApiServices.Controllers
{
    public class StockInCountCheck_RequestParams
    {
        public string id { get; set; }
        public string name { get; set; }



    }
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

       [Route("api/StockIn/StockInCount")]
       [HttpPost]
        public int StockInCount(StockIn stock)
        {
            var list = new List<StockIn>()
            {
                new StockIn{ Id=1,IsActive=true,NickName="rams11",TotalQuantity=101,
                    CreatedDate =DateTime.Now.ToShortDateString(), VendorId =1},

                 new StockIn{ Id=1,IsActive=true,NickName="rams12",TotalQuantity=107,
                    CreatedDate =DateTime.Now.ToShortDateString(), VendorId =2},
            };

            var countOfRecoreds = list.Where(e => e.Id.Equals(stock.Id)).Count();
            return countOfRecoreds;
        }

        [HttpPost]
        [Route("api/StockIn/AddStock")]
        public IHttpActionResult AddStock(StockIn data)
        {
            //return new string[] { "value1", "value2" };
            // return new Vishnu { name = "rams", password = "rams1" };
            Console.WriteLine(data);
            return Ok(data);

        }


        [Route("api/StockIn/GetStockForToday")]
        [HttpGet]
        public List<StockIn> GetStockForToday()
        {
            var list = new List<StockIn>()
            {
                new StockIn{ Id=1,IsActive=true,NickName="patnam1",TotalQuantity=101,
                    CreatedDate =DateTime.Now.ToShortDateString(), VendorId =1},
                 new StockIn{ Id=2,IsActive=true,NickName="patnam2",TotalQuantity=107,
                    CreatedDate =DateTime.Now.ToShortDateString(), VendorId =2},
            };


         
            return list;
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
