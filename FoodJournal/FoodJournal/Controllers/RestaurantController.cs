using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repository;
using DataLayer.Class;
using DataLayer;

namespace FoodJournal.Controllers
{
    public class RestaurantController : Controller
    {
        private RestaurantRepository repo = new RestaurantRepository();
        private MenuRepository menurepo = new MenuRepository();
        private OfferRepository offerrepo = new OfferRepository();
        private PostRepository postrepo = new PostRepository();
        // GET: Restaurant

        [HttpGet]
        public ActionResult MyRestaurants()
        {

            return View(this.repo.GetRestaurantsByUserId(Convert.ToInt32(Session["UserID"])));
        }
  

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Restaurant restaurant)
        {
            restaurant.UserID = Convert.ToInt32(Session["UserID"]);
            restaurant.Status = 1;
            this.repo.Insert(restaurant);
            return RedirectToAction("MyRestaurants", "Restaurant");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(this.repo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Restaurant restaurant)
        {
            this.repo.Update(restaurant);
            return RedirectToAction("MyRestaurants", "Restaurant");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(this.repo.Get(id));
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            this.repo.Delete(id);
            return RedirectToAction("MyRestaurants", "Restaurant");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(this.repo.Get(id));
        }


        [HttpGet]
        public ActionResult ShowAllRestaurants()
        {
            return View(this.repo.GetAll());
        }


        [HttpGet]
        public ActionResult MenusAndOffers(int id)
        {
            
            ViewData["RestaurantReviews"] = postrepo.GetPostsByRestaurantId(id);
            ViewData["RestaurantInfo"] = repo.Get(id);
            ViewData["Offers"] = offerrepo.getAllOffersByRestaurantId(id);
            return View(this.menurepo.getAllMenusByRestaurantId(id));
        }


        [HttpGet]
        public ActionResult SearchByRestaurantName(string id)
        {
            return View("ShowAllRestaurants", this.repo.GetAllRestaurantByName(id));
        }


        [HttpGet]
        public ActionResult SearchByRestaurantLocation(string id)
        {
            return View("ShowAllRestaurants", this.repo.GetAllRestaurantByLocation(id));
        }

    }
}