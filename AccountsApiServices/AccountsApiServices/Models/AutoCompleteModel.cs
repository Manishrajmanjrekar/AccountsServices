using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountsApiServices.Models
{
    public class AutoCompleteModel
    {
        public string q { get; set; }
        public string sort { get; set; }

        public string order { get; set; }
    }
}