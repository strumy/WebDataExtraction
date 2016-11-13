using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;
using Scrapper;
using System.Web;
using System.Web.Script.Serialization;
using System;
using System.IO;

namespace WebDataExtraction.Controllers
{
    public class SearchController : Controller
    {        
        public ActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public JsonResult Get(string item, string location)
        {            
            Scrapper.Scrapper scrapper = new Scrapper.Scrapper(item, location);
            List<Scrapper.Restaurant> restaurents = new List<Restaurant>();

            Restaurant restaurant;
            for (int i = 0; i < 10; i++)
            {
                restaurant = new Restaurant();
                restaurant.Name = "Name_" + i;
                restaurant.Address = "Address_" + i;
                restaurant.Zipcode = "ZipCode_" + i;
                restaurents.Add(restaurant);
            }

            return Json(restaurents, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(string item, string location, string searchResult)
        {
            /*
            var jsonSerializer = new JavaScriptSerializer();
            var jsonString = String.Empty;

            context.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(context.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }

            var restaurentList = jsonSerializer.Deserialize<List<string>>(jsonString);*/

            int len = 2;
            if (searchResult != null)
            {
                len = 1;
            }                


            if (len > 0)
            {                
                return Json(new { success = true, responseText = "Data Saved Successfully." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, responseText = "Data was NOT Saved Successfully." }, JsonRequestBehavior.AllowGet);
        }
    }
}