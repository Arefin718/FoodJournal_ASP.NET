using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.IO;
using DataLayer.Repository;
using DataLayer.Class;

namespace FoodJournal.Controllers
{
    public class AdminProfileController : Controller
    {
        private AdminRepository repo = new AdminRepository();
        // GET: AdminProfile
        public ActionResult view(int id)
        {
            if (Session["AdminID"] != null)
            {
                return View(this.repo.Get(id));
            }else
            {
                return RedirectToAction("Login","admin");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["AdminID"] != null)
            {
                return View(this.repo.Get(id));
            }
            else
            {
                return RedirectToAction("Login", "admin");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Admin admin)
        {
            this.repo.Update(admin);
            Session["AdminName"] =admin.Name;
            Session["AdminEmail"] = admin.Email;
            return RedirectToAction("view", "adminprofile", new { id = admin.AdminID });
        }


        [HttpGet]
        public ActionResult PassChange(int id)
        {

            if (Session["AdminID"] != null)
            {

                TempData["AdminID"] = id;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "admin");
            }

        }


        [HttpPost]
        public ActionResult  PassChange(Admin admin)
        {
            
            
            Admin ad = this.repo.ChangePassword(admin);
          
            
                   Session["AdminID"] = ad.AdminID;
                    Session["AdminName"] = ad.Name.ToString();
                    Session["AdminEmail"] = ad.Email;
                    Session["AdminPass"] =  ad.Password;
                    Session["AdminImage"] = ad.Image; 
            return RedirectToAction("view", "adminProfile", new { id = ad.AdminID });
            
        }

        [HttpGet]
        public ActionResult ProPicChange(int id)
        {
            if (Session["AdminID"] != null)
            {

                TempData["AdminID"] = id;
                Admin admin = this.repo.Get(id);
                return View(admin);
            }
            else
            {
                return RedirectToAction("Login", "admin");
            }
        }


        [HttpPost]
        public ActionResult ProPicChange(Admin admin, HttpPostedFileBase Image)
        {

            if (Image != null)
            {
                Admin image = repo.Get(admin.AdminID);
                string tmp = Path.Combine(Server.MapPath(image.Image.ToString())).ToString();
                System.IO.File.Delete(tmp);

                String sExt = Path.GetExtension(Image.FileName).ToLower();
                String imagePath = "/Content/images/admin/" + Session["AdminEmail"].ToString() + sExt;
                // var fileName = Path.GetFileName(Image.FileName);
                string temp = Session["AdminEmail"].ToString() + sExt;
                var path = Path.Combine(Server.MapPath("~/Content/images/admin"), temp);
                Image.SaveAs(path);

                admin.Image = imagePath;

                Session["AdminImage"] = imagePath;
                this.repo.ProfilePictureChange(admin);
                return RedirectToAction("view", "adminProfile", new { id = admin.AdminID });
            }
            //fileUpload
            else
            {
                return RedirectToAction("view", "adminProfile", new { id = admin.AdminID });
            }
           
        }




        [HttpGet]
        public ActionResult Logout()
        {
           
            Session.Clear();
            return RedirectToAction("index", "admin");
        }



    }
}