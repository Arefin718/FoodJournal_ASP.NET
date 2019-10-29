using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repository;
using DataLayer.Class;
using System.IO;
using DataLayer;

namespace FoodJournal.Controllers
{
    public class AdminController : Controller
    {
        private AdminRepository adminRepo = new AdminRepository();
        private RestaurantRepository resRepo = new RestaurantRepository();
        private UserRepository userRepo = new UserRepository();


        // GET: Admin
        public ActionResult Index()
        {
            if (Session["AdminID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "admin");
            }
           
        }

        [HttpGet]
        public ActionResult List()
        {
            if (Session["AdminID"] != null)
            {
                return View(this.adminRepo.GetAll());
            }
            else
            {
                return RedirectToAction("Login", "admin");
            }
            
        }

        [HttpGet]
        public ActionResult Create()
        {
           
            if (Session["AdminID"] != null)
            {
                this.TempData["Error"] = "";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "admin");
            }
           
        }

        [HttpPost]
        public ActionResult Create(Admin admin, HttpPostedFileBase Image)
        {
            Admin logAdmin = this.adminRepo.GetByEmail(admin.Email.ToString());
            if (logAdmin != null)
            {

                this.TempData["Error"] = "Email already exist";
                return View();

            }
            else
            {
                String sExt = Path.GetExtension(Image.FileName).ToLower();
                String imagePath = "/Content/images/admin/" + admin.Email + sExt;
                // var fileName = Path.GetFileName(Image.FileName);
                string temp = admin.Email + sExt;
                var path = Path.Combine(Server.MapPath("~/Content/images/admin"), temp);
                Image.SaveAs(path);

                admin.Image = imagePath;

                this.adminRepo.Insert(admin);

                return RedirectToAction("index", "admin");

            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            Admin logAdmin = this.adminRepo.GetByEmail(admin.Email);
           
            if (logAdmin != null)
            {
                if (admin.Email == logAdmin.Email && logAdmin.Password == admin.Password)
                {

                    Session["AdminID"] = logAdmin.AdminID;
                    Session["AdminName"] = logAdmin.Name.ToString();
                    Session["AdminEmail"] = logAdmin.Email;
                    Session["AdminPass"] = logAdmin.Password;
                    Session["AdminImage"] = logAdmin.Image;


                    return RedirectToAction("index", "admin");
                }
                else
                {
                    this.TempData["ErrorMessage"] = "Incorrect Email or Password";
                    return RedirectToAction("login", "admin");
                }
            }
            else
            {
                this.TempData["ErrorMessage"] = "Incorrect Email or Password";

                return RedirectToAction("login", "admin");
            }


        }


        [HttpGet]
        public ActionResult AllRestaurants()
        {
            if (Session["AdminID"] != null)
            {
                return View(this.resRepo.GetAllByAdmin());
            }
            else
            {
                return RedirectToAction("Login", "admin");
            }

           
        }




        [HttpGet]
        public ActionResult RestaurantsByMostReview()
        {
            if (Session["AdminID"] != null)
            {
                return View("AllRestaurants", this.resRepo.RestaurantByMostUseReview());
            }
            else
            {
                return RedirectToAction("Login", "admin");
            }


        }


        public ActionResult AllUsers()
        {
            return View(this.userRepo.AllUsersByAdmin());
        }
    }
}