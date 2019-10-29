using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repository;
using DataLayer.Class;

namespace FoodJournal.Controllers
{
    public class AjaxController : Controller
    {
        private RestaurantRepository repo = new RestaurantRepository();

        [HttpGet]
        public JsonResult GetRestaurants(string term)
        {

            List<Restaurant> allRestaurants = this.repo.GetAll();


            List<Restaurant> matchedRestaurants = allRestaurants.Where(s => s.RestaurantName.ToLower().Contains(term)).ToList();

            return Json(matchedRestaurants.ToList(), JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetRestaurantsByLocation(string term)
        {

            List<Restaurant> allRestaurants = this.repo.GetAll();


            List<Restaurant> matchedRestaurants = allRestaurants.Where(s => s.Location.ToLower().Contains(term)).ToList();

            return Json(matchedRestaurants.ToList(), JsonRequestBehavior.AllowGet);

        }
    }
}