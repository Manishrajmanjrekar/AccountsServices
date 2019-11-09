using System;
using System.Collections.Generic;

namespace ApiCoreServices.EfDbContext
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
    }
}