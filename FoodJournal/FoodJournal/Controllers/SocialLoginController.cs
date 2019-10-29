using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repository;
using DataLayer;

namespace FoodJournal.Controllers
{
    public class SocialLoginController : Controller
    {
        private UserRepository userRepo = new UserRepository();
        // GET: SocialLogin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GoogleLogin(User user)
        {
            user.Status = 1;
            Session["UserID"] = this.userRepo.Insert(user);

            Session["UserName"] = user.Name;
            Session["UserImage"] = user.Image;
            Session["UserEmail"] = user.Email;
            Session["UserPass"] = user.Password;
            Session["UserLatitude"] = null;
            Session["UserLongitude"] = null;


            return RedirectToAction("Index", "Home");

        }
    }
}