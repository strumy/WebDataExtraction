﻿using System.Collections.Generic;
using System.Web.Mvc;

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

            List<Scrapper.Restaurant> restaurents = scrapper.GetResult();

            return Json(restaurents, JsonRequestBehavior.AllowGet);
        }
    }
}