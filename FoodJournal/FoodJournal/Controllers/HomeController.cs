using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.Repository;

namespace FoodJournal.Controllers
{
    public class HomeController : Controller
    {
     
        // GET: Home
        private PostRepository repo = new PostRepository();
        private OfferRepository repoOffer = new OfferRepository();
        public ActionResult Index()
        {
            ViewData["Offers"] = this.repoOffer.GetAll();
            return View(this.repo.GetAll());
        }
    }
}