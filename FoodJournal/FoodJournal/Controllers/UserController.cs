using DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodJournal.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        private UserRepository repo = new UserRepository();

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registration(User user, HttpPostedFileBase Image)
        {

            if (this.repo.EmailExists(user.Email) == false)
            {
                //image upload

                String sExt = Path.GetExtension(Image.FileName).ToLower();
                String imagePath = "/Content/images/user/" + Image.FileName.Replace(sExt, "") + sExt + DateTime.Now.ToString("ddMMMyyhhmmsstt") + sExt;
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/user"), fileName);
                Image.SaveAs(path + DateTime.Now.ToString("ddMMMyyhhmmsstt") + sExt);





                user.Image = imagePath;
                user.Status = 1;

                Session["UserID"] = this.repo.Insert(user);

                Session["UserName"] = user.Name;
                Session["UserEmail"] = user.Email;
                Session["UserImage"] = imagePath;
                Session["UserPass"] = user.Password;
                Session["UserLatitude"] = user.Latitude;
                Session["UserLongitude"] = user.Longitude;

                return RedirectToAction("index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "User exists if u forgot ur password go to login page to recover";
                return RedirectToAction("Registration", "User");
            }

            
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("index", "home");
        }


        [HttpPost]
        public ActionResult Login(User user)
        {
           User logUser = this.repo.GetByEmail(user.Email);

            if (logUser != null)
            {
                if (user.Email == logUser.Email && logUser.Password == user.Password)
                {
                    if (logUser.Status == 1)
                    {

                        Session["UserID"] = logUser.UserID;
                        Session["UserName"] = logUser.Name.ToString();
                        Session["UserEmail"] = logUser.Email;
                        Session["UserPass"] = logUser.Password;
                        Session["UserImage"] = logUser.Image;
                        Session["UserLatitude"] = logUser.Latitude;
                        Session["UserLongitude"] = logUser.Longitude;

                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        this.TempData["ErrorMessage"] = "Your Id is temporarily Blocked";
                        return RedirectToAction("login", "user");
                    }

                }
                else
                {
                    this.TempData["ErrorMessage"] = "Incorrect Email or Password";
                    return RedirectToAction("login", "user");
                }
            }
            else
            {
                this.TempData["ErrorMessage"] = "Incorrect Email or Password";
  
                return RedirectToAction("login", "user");
            }
            
     
        }

    }
}