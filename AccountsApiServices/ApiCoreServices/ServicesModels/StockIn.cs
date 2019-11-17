using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCoreServices.Models
{
    public class StockInViewModel
    {
        public long id { get; set; }
        public long vendorId { get; set; }
        public string loadName { get; set; }
        public int totalQuantity { get; set; }
        public bool isActive { get; set; }        
        public DateTime? createdDate { get; set; }

        public string formattedCreatedDate { get; set; }
        public string formattedModifiedDate { get; set; }
        
        /// <summary>
        /// vendor nick name
        /// </summary>
        public string nickName { get; set; }
        
        /// <summary>
        /// vendor first name
        /// </summary>
        public string firstName { get; set; }
        

    }
}