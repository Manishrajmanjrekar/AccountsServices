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

        [Route("api/Sales/SalesByStockId")]
        [HttpPost]
        public List<Sales> SalesByStockId([FromBody] string id)
        {
            //var data = new VendorPaymentModel();

            var stockInId = Convert.ToInt16(id);
            var objdata = new List<Sales>()
            {
                new Sales
                {
                Id = 1,
                Price = 7500,
                VendorName = "rams1",
                CustomerName ="Feroz",
                Quantity = 10,
                CreatedDate = DateTime.Now.ToShortDateString(),
                Total = 75000,
                VendorId = 1,
                LoadName = "load1",

                  },
                 new Sales
                {
                Id = 1,
                Price = 7500,
                VendorName = "rams1",
                CustomerName ="kader Bhai",
                Quantity = 10,
                CreatedDate = DateTime.Now.ToShortDateString(),
                Total = 75000,
                VendorId = 1,
                LoadName = "load1",

                  },
                new Sales
                {
                Id = 1,
                Price = 800,
                VendorName = "rams1",
                CustomerName ="Afzal",
                Quantity = 10,
                CreatedDate = DateTime.Now.ToShortDateString(),
                Total = 8000,
                VendorId = 1,
                LoadName = "load1",

                  }
               };

            var obj = objdata.Where(e => e.Id.Equals(stockInId)).Select(p => p).ToList();
            //data.SalesList = obj;
            //data.VendorLoadExpenseList = new List<VendorLoadExpense>()
            //{
            //    new VendorLoadExpense {
            //        stockInId =1, ExpensesCategoryName ="Bhoonsa", CreatedDate=DateTime.Today.ToString(),
            //    Cost=170,ExpensesCategoryId=1
            //    },
            //    new VendorLoadExpense {
            //        stockInId =2, ExpensesCategoryName ="chaars", CreatedDate=DateTime.Today.ToString(),
            //    Cost=170,ExpensesCategoryId=2
            //    },
            //    new VendorLoadExpense {
            //        stockInId =1, ExpensesCategoryName ="lorryRent", CreatedDate=DateTime.Today.ToString(),
            //    Cost=7500,ExpensesCategoryId=3
            //    },
            //    new VendorLoadExpense {
            //        stockInId =2, ExpensesCategoryName ="Food", CreatedDate=DateTime.Today.ToString(),
            //    Cost=1550,ExpensesCategoryId=4
            //    },
            //}
            return obj;

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
