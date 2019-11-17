﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCoreServices.ViewModels
{
    public class CommonResponseViewModel
    {
        public bool isSuccess { get; set; }

        public int statusCode { get; set; }

        public string message { get; set; }

        public long recordId { get; set; }
    }
}