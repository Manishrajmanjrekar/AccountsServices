using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountsApiServices.ViewModels
{
    public class CommonResponseViewModel
    {
        public bool isSuccess { get; set; }

        public int statusCode { get; set; }

        public string message { get; set; }

        public int recordId { get; set; }
    }
}