using AccountsApiServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountsApiServices.Controllers
{
    public class VendorExpensesController : ApiController
    {
        // GET: api/VendorExpenses
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/VendorExpenses/VendorExpensesByStockInId")]
        [HttpPost]
        public List<VendorLoadExpense> VendorExpensesByStockInId([FromBody] string id)
        {
            var stockInId = Convert.ToInt16(id);
          

            var data = new List<VendorLoadExpense>()
            {
                new VendorLoadExpense {
                    stockInId =1, ExpensesCategoryName ="Bhoonsa", CreatedDate=DateTime.Today.ToString(),
                Amount=170,ExpensesCategoryId=1,VendorName="ArjunBhai"
                },
                new VendorLoadExpense {
                    stockInId =1, ExpensesCategoryName ="chaars", CreatedDate=DateTime.Today.ToString(),
                Amount=170,ExpensesCategoryId=2,VendorName="ArjunBhai"
                },
                new VendorLoadExpense {
                    stockInId =1, ExpensesCategoryName ="lorryRent", CreatedDate=DateTime.Today.ToString(),
                Amount=7500,ExpensesCategoryId=3,VendorName="ArjunBhai"
                },
                new VendorLoadExpense {
                    stockInId =1, ExpensesCategoryName ="Food", CreatedDate=DateTime.Today.ToString(),
                Amount=1550,ExpensesCategoryId=4,VendorName="ArjunBhai"
                }
            };
            var obj = data.Where(e => e.stockInId.Equals(stockInId)).Select(p => p).ToList();
            return obj;
        }

        // POST: api/VendorExpenses
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/VendorExpenses/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/VendorExpenses/5
        public void Delete(int id)
        {
        }
    }
}
