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

namespace AccountsApiServices.Controllers
{
   

    public class VendorController : ApiController
    {
        // GET api/values
        public Vendor Get()
        {
            

            return new Vendor { firstName = "rams", lastName = "rams1" };

        }

        [Route("api/Vendor/Getdata")]
        public async void Getdata()
        {
            //return new string[] { "value1", "value2" };
            using (HttpClient client = new HttpClient())
            {
                const string url = "https://github.com/tugberkugurlu/ASPNETWebAPISamples/archive/master.zip";
                using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    string fileToWriteTo = Path.GetTempFileName();
                    using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                    {
                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                    }
                }
            }
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

        //[HttpPost]
        //public IHttpActionResult UploadFiles()
        //{
        //    //[FromBody] Vishnu data
        //    //Console.WriteLine(data);
        //    var contentType = Request.Content.Headers.ContentType.MediaType;
        //    var requestParams = Request.Content.ReadAsStringAsync().Result;

        //    System.Web.HttpRequest httpRequest = System.Web.HttpContext.Current.Request;
        //    System.Collections.Specialized.NameValueCollection formData = httpRequest.Form;
        //    var name = Convert.ToString(formData["test"]);
        //    var name1 = Convert.ToString(formData["test1"]);


        //    var rams = HttpContext.Current.Request.Params["rams"];
        //    var rams1 = HttpContext.Current.Request.Params["rams1"];
        //    Console.WriteLine();
        //    return Ok();
        //}

        //[HttpPost]
        //[Route("api/Vendor/UploadFilesWithData")]
        //public async Task<string> UploadFilesWithData()
        //{
        //    try
        //    {
        //        if (!Request.Content.IsMimeMultipartContent())
        //        {
        //            //return StatusCode(HttpStatusCode.UnsupportedMediaType);
        //        }


        //        string root = HttpContext.Current.Server.MapPath("~/App_Data");
        //        var provider = new MultipartFormDataStreamProvider(root);
        //        var result = await Request.Content.ReadAsMultipartAsync(provider);

        //        foreach (var key in provider.FormData.AllKeys)
        //        {
        //            foreach (var val in provider.FormData.GetValues(key))
        //            {
        //                if (key == "companyName")
        //                {
        //                    var companyName = val;
        //                }
        //            }
        //        }


        //        var filesReadToProvider = Request.Content.ReadAsMultipartAsync().Result;

        //        foreach (var stream in filesReadToProvider.Contents)
        //        {
        //            // Getting of content as byte[], picture name and picture type
        //            var fileBytes = stream.ReadAsByteArrayAsync().Result;
        //            var pictureName = stream.Headers.ContentDisposition.FileName;
        //            var contentType = stream.Headers.ContentType.MediaType;
        //        }

        //        var rams = HttpContext.Current.Request.Params["rams"];
        //        var rams1 = HttpContext.Current.Request.Params["rams1"];
        //        Console.WriteLine();

        //        var result1 = Request.Content.ReadAsMultipartAsync().Result;

        //        var requestJson = result1.Contents[0].ReadAsStringAsync().Result;
        //        // var request = JsonConvert.DeserializeObject<MyRequestType>(requestJson);

        //        if (result1.Contents.Count > 1)
        //        {
        //            var fileByteArray = result1.Contents[1].ReadAsByteArrayAsync().Result;

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }


        //    return null;
        //}

        //[HttpPost]
        //[Route("api/Vendor/AddVendor")]
        //public IHttpActionResult AddVendor(Vendor data)
        //{
        //    try
        //    {
               

                
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }


        //    return null;
        //}

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
