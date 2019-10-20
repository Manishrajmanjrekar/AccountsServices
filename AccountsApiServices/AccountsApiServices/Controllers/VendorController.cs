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

    public class VendorNameSearch_RequestParams
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
        public List<Vendor> VendorNames(VendorNameSearch_RequestParams data)
        {
            Console.WriteLine(data);

            var objdata = new List<Vendor>()
            {
                new Vendor { firstName = "rams", lastName = "rams11"+":1" },
                new Vendor { firstName = "rams1", lastName = "rams12"+":2" },

                new Vendor { firstName = "rams2", lastName = "rams13"+":3" },

                new Vendor { firstName = "rams3", lastName = "rams13" +":4"}



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

            if (vendorVM.id == 0)
            {
                // insert
                vendorVM.id = vendors[vendors.Count - 1].id + 1;
                //vendorVM.Url = "/vendor/" + vendorVM.id;
                vendors.Add(vendorVM);

                response.isSuccess = true;
                response.recordId = vendorVM.id;
            }
            else
            {
                // update
                VendorViewModel vendor = vendors.Where(x => x.id == vendorVM.id).FirstOrDefault();

                if (vendor != null)
                {
                    vendor.firstName = vendorVM.firstName;
                    vendor.address = vendorVM.address;
                    vendor.city = vendorVM.city;
                    vendor.referredBy = vendorVM.referredBy;
                    vendor.mobile = vendorVM.mobile;

                    response.isSuccess = true;
                    response.recordId = vendorVM.id;
                }
                else // TO DO: remove this temp else block
                {
                    vendorVM.id = vendors[vendors.Count - 1].id + 1;
                    //vendorVM.Url = "/vendor/" + vendorVM.id;
                    vendors.Add(vendorVM);

                    response.isSuccess = true;
                    response.recordId = vendorVM.id;
                }
            }

            //if (response.IsSuccess)
            //{
            //    _cache.Set("Vendors", vendors);
            //}

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
                //_cache.Set("Vendors", vendors);

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

            List<string> exitingNickNames = new List<string>()
            {
                "ramesh",
                "suresh",
                "ajay",
                "vijay",
            };

            if (exitingNickNames.Contains(data, StringComparer.OrdinalIgnoreCase))
            {
                isDuplicateNickName = true;
            }

            return isDuplicateNickName;
        }

        private List<VendorViewModel> GetVendors()
        {
            List<VendorViewModel> vendors = null;
            //_cache.TryGetValue("Vendors", out vendors);

            if (vendors == null)
            {
                vendors = new List<VendorViewModel>
                {
                    new VendorViewModel { id = 1, firstName = "Arjun", address="Dilshuknagar", city="Hyderabad", referredBy="Referrer 1", mobile = "9923456789"},
                    new VendorViewModel { id = 2, firstName = "Rizwan", address="Nagole", city="Hyderabad", referredBy="Referrer 1", mobile = "8823456789"},
                    new VendorViewModel {
                         id = 3,
                         nickName = "AjayTej",
                         firstName = "Ajay",
                         middleName = "mn",
                         lastName = "Teja",
                         mobile = "9999999999",
                         alternateMobile = "8888888888",
                         homePhone = "8666666666",
                         officePhone = "8555555555",
                         email = "Ajay@abc.com",
                         address = "Ajay address",
                         city = "Ajay city",
                         state = "Ajay state",
                         shopName = "Ajay shop",
                         shopLocation = "Ajay shop ",
                         referredBy = "Ajay referrer",
                    }
                };

                //vendors.ForEach(x => x.Url = "/vendor/" + x.VendorId);


                //_cache.Set("Vendors", vendors);
            }

            return vendors;
        }
    }
}
