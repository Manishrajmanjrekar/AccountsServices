using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AccountsApiServices.Models;
using AccountsApiServices.ViewModels;

namespace AccountsApiServices.Controllers
{

    public class RequestParams
    {
        public string q { get; set; }
        public string sort { get; set; }

        public string order { get; set; }

    }
    public class VendorController : ApiController
    {
        
        //// GET api/values
        //public Vendor Get()
        //{
        //    return new Vendor { firstName = "rams", lastName = "rams1" };
        //}

        [Route("api/Vendor/VendorNames")]
        [HttpPost]
        public List<Vendor> VendorNames(RequestParams data)
        {
            Console.WriteLine(data);

            var objdata = new List<Vendor>()
            {
                new Vendor { firstName = "rams", lastName = "rams11" },
                new Vendor { firstName = "rams1", lastName = "rams12" },

                new Vendor { firstName = "rams2", lastName = "rams13" },

                new Vendor { firstName = "rams3", lastName = "rams13" }



            };

            var obj = objdata.Where(e => e.firstName.Contains(data.q)).ToList();
            return obj;

        }

       

        [HttpPost]
        [Route("api/Vendor/AddVendor")]
        public IHttpActionResult AddVendor(Vendor data)
        {
            //return new string[] { "value1", "value2" };
            // return new Vishnu { name = "rams", password = "rams1" };
           Console.WriteLine(data);
           return Ok(data);

        }



        [HttpGet]
        [Route("api/Vendor")]
        public List<VendorViewModel> Get()
        {
            return GetVendors();
        }

        [HttpGet]
        [Route("api/Vendor/{id}")]
        public VendorViewModel Get(int id)
        {
            VendorViewModel vendor = null;

            List<VendorViewModel> vendors = GetVendors();
            vendor = vendors.Where(x => x.id == id).FirstOrDefault();

            return vendor;
        }

        [HttpPost]
        [Route("api/Vendor/SaveVendor")]
        public CommonResponseViewModel SaveVendor(VendorViewModel vendorVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            List<VendorViewModel> vendors = GetVendors();           

            if (vendorVM.id <= 0)
            {
                // insert
                vendorVM.id = vendors[vendors.Count - 1].id + 1;
                vendors.Add(vendorVM);

                response.isSuccess = true;
                response.recordId = vendorVM.id;
            }
            else
            {
                // update
                var vendor = vendors.Where(x => x.id == vendorVM.id).FirstOrDefault();
                if (vendor != null)
                {
                    int index = vendors.FindIndex(x => x.id == vendorVM.id);
                    vendors[index] = vendorVM;

                    response.isSuccess = true;
                    response.recordId = vendorVM.id;
                   
                }
            }

            if (response.isSuccess)
            {
                HttpContext.Current.Cache.Remove("Vendors");
                HttpContext.Current.Cache.Insert("Vendors", vendors);
            }

            return response;
        }

        [HttpDelete]
        [Route("api/Vendor/Delete/{id}")]
        public bool DeleteVendor(int id)
        {
            bool isSuccess = false;
            List<VendorViewModel> vendors = GetVendors();
            if (id > 0 && vendors != null)
            {
                vendors = vendors.Where(x => x.id != id).ToList();
                HttpContext.Current.Cache.Remove("Vendors");
                HttpContext.Current.Cache.Insert("Vendors", vendors);

                isSuccess = true;
            }

            return isSuccess;
        }

        [Route("api/Vendor/CheckIsDuplicateNickName")]
        [HttpPost]
        public bool CheckIsDuplicateNickName([FromBody]string data)
        {
            bool isDuplicateNickName = false;

            if (string.IsNullOrWhiteSpace(data))
                return isDuplicateNickName;

            var vendors = GetVendors();
            if (vendors != null && vendors.Any(x => x.nickName.ToLower() == data.ToLower()))
            {
                isDuplicateNickName = true;
            }

            return isDuplicateNickName;
        }

        private List<VendorViewModel> GetVendors()
        {
            List<VendorViewModel> vendors = null;
            if (HttpContext.Current.Cache.Get("Vendors") != null)
            {
                vendors = (List<VendorViewModel>)HttpContext.Current.Cache.Get("Vendors");
            }

            if (vendors == null || vendors.Count == 0)
            {
                List<string> addresses = new List<string>()
                { "Dilsukhnagar","LBnagar","Nagole","Uppal","Balnagar","Gandipet","Hayathnagar","Rajendranagar","Saroornagar" };

                List<string> vendorNames = new List<string>()
                {
                    "Abdul","Shivansh","Avi","Samar","Pratyush","Viaan","Neel","Om","Anirudh","Isaac","Devansh","Yash","Abeer","Rehaan",
                    "Tejas","Zayan","Advay","Sarthak","Darsh","Anay","Gautam","Jason","Agastya","Rohan","Daksh","Manan","Pranav",
                    "Abhiram","Samarth"
                };

                List<string> lastNames = new List<string>()
                {
                    "Sharma","Verma","Gupta","Malhotra","Bhatnagar","Saxena","Kapoor","Singh","Mehra","Chopra","Sarin",
                    "Dutt","Rao","Singh","Yadav","Jhadav","Jaiteley","Chauhan","Khan"
                };


                Random random = new Random();
                
                vendors = new List<VendorViewModel>();
                VendorViewModel vendor = new VendorViewModel();
                
                for (int recordId = 1; recordId <= 100; recordId++)
                {                   
                    string name = vendorNames[random.Next(1, vendorNames.Count())];

                    vendor = new VendorViewModel();
                    vendor.id = recordId;
                    vendor.nickName = name + recordId;
                    vendor.firstName = name;
                    vendor.middleName = "";
                    vendor.lastName = lastNames[random.Next(1, lastNames.Count())];
                    vendor.mobile = ("999999999" + recordId);
                    vendor.alternateMobile = ("888888888" + recordId);
                    vendor.homePhone = ("777777777" + recordId);
                    vendor.email = name + "@abc.com";
                    vendor.address = addresses[random.Next(1, addresses.Count())];
                    vendor.city = "Hyderabad";
                    vendor.state = "Telangana";
                    vendor.referredBy = "Referrer " + random.Next(1, 10).ToString();

                    vendor.mobile = vendor.mobile.Length > 10 ? vendor.mobile.Substring(vendor.mobile.Length-10, 10) : vendor.mobile;
                    vendor.alternateMobile = vendor.alternateMobile.Length > 10 ? vendor.alternateMobile.Substring(vendor.alternateMobile.Length - 10, 10) : vendor.alternateMobile;
                    vendor.homePhone = vendor.homePhone.Length > 10 ? vendor.homePhone.Substring(vendor.homePhone.Length - 10, 10) : vendor.homePhone;

                    vendors.Add(vendor);
                };

                HttpContext.Current.Cache.Insert("Vendors", vendors);
            }

            return vendors;
        }
    }
}
