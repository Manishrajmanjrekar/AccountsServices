using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCoreServices.ViewModels
{
    public class ExpenseTypeViewModel
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class ExpenseViewModel
    {
        public long id { get; set; }
        public long expenseTypeId { get; set; }
        public string name { get; set; }

        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public string modifiedBy { get; set; }

        public string expenseTypeName { get; set; }
        public string formattedCreatedDate { get; set; }
        public string formattedModifiedDate { get; set; }        
    }
}