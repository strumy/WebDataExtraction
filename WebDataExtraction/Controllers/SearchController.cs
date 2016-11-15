using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;
using Scrapper;
using System.Web;
using System.Web.Script.Serialization;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using WebDataExtraction.Context;
using WebDataExtraction.Entity;

namespace WebDataExtraction.Controllers
{
    public class SearchController : Controller
    {
        private SearchContext db = new SearchContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get(string item, string location)
        {
            Scrapper.Scrapper scrapper = new Scrapper.Scrapper(item, location);
            //List<Scrapper.Restaurant> restaurents = scrapper.GetResult();

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
        public JsonResult Save(string item, string location, List<Restaurant> searchResult)
        {
            if (searchResult == null || item.Trim() == "" || location.Trim() == "")
            {
                return Json(new { success = false, responseText = "Data was NOT Saved. Restaurent Name, Location or Search data was empty." }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                SearchData searchData = new SearchData();
                searchData.Name = item;
                searchData.Location = location;
                db.SearchDatas.Add(searchData);

                foreach (var restaurent in searchResult)
                {                        
                    RestaurentData restaurentData = new RestaurentData();
                    restaurentData.Name = restaurent.Name;
                    restaurentData.Address = restaurent.Address;
                    restaurentData.Zipcode = restaurent.Zipcode;
                    restaurentData.SearchDataId = searchData.SearchDataId;
                    db.RestaurentDatas.Add(restaurentData);                        
                }

                if (db.SaveChanges() > 0)
                {
                    return Json(new { success = true, responseText = "Data Saved Successfully." }, JsonRequestBehavior.AllowGet);
                }
                
                return Json(new { success = false, responseText = "Data was NOT Saved Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Import(string item, string location)
        {
            if (item.Trim() == "" || location.Trim() == "")
            {
                return Json(new { success = false, responseText = "Could not import data. Name or Location was empty." }, JsonRequestBehavior.AllowGet);
            }                        

            try
            {
                var searchDatas = db.SearchDatas.Where(s => s.Name == item && s.Location == location).Include(c => c.RestaurentDatas).FirstOrDefault();

                if (searchDatas == null)
                {
                    return Json(new { success = false, responseText = "Could not import data. No data was found for your search criteria (name = " + item + " and location =" + location + ")." }, JsonRequestBehavior.AllowGet);
                }

                if (searchDatas.RestaurentDatas == null)
                {
                    return Json(new { success = false, responseText = "Could not import data. No data was found for your search criteria (name = " + item + " and location =" + location + ")." }, JsonRequestBehavior.AllowGet);
                }

                List<RestaurentData> restaurentDatas = new List<RestaurentData>();
                List<Scrapper.Restaurant> restaurents = new List<Restaurant>();

                Restaurant sRestaurant;

                foreach (var restaurent in searchDatas.RestaurentDatas)
                {
                    sRestaurant = new Restaurant();
                    sRestaurant.Name = restaurent.Name;
                    sRestaurant.Address = restaurent.Address;
                    sRestaurant.Zipcode = restaurent.Zipcode;
                    restaurents.Add(sRestaurant);
                }
                return Json(restaurents, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);                
            }            
        }
    }
}