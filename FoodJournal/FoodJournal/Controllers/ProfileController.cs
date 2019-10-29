using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.IO;
using DataLayer.Repository;

namespace FoodJournal.Controllers
{
    public class ProfileController : Controller
    {
        private UserRepository repo = new UserRepository();
        private PostRepository postRepo = new PostRepository();
        private RecipeRepository recipeRepo = new RecipeRepository();
        // GET: Profile

        [HttpGet]
        public ActionResult view(int id)
        {

            if (Session["UserID"] != null)
            {
                ViewData["posts"] = this.postRepo.GetPostByUserId(id);

                ViewData["recipes"] = this.recipeRepo.GetRecipeByUserId(id);

                return View(this.repo.Get(id));
            }
            else
            {
                return RedirectToAction("Login", "user");
            }

           
  
            //return View(this.repo.Get(Int32.Parse(Session["UserID"].ToString())));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {


            if (Session["UserID"] != null)
            {
                return View(this.repo.Get(id));
            }
            else
            {
                return RedirectToAction("Login", "user");
            }
         
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {  
                this.repo.Update(user);
                return RedirectToAction("view", "profile", new { id=user.UserID});    
        }


        [HttpGet]
        public ActionResult PassChange(int id)
        {
          
            if (Session["UserID"] != null)
            {
                TempData["AdminID"] = id;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "user");
            }
        }


        [HttpPost]
        public ActionResult PassChange(User user)
        {
            User u = this.repo.ChangePassword(user);

            Session["UserID"] = u.UserID;
            Session["UserName"] = u.Name;
            Session["UserEmail"] = u.Email;
            Session["UserPass"] = u.Password;
            Session["UserLatitude"] = u.Latitude;
            Session["UserLongitude"] = u.Longitude;

            return RedirectToAction("view", "Profile", new { id=u.UserID});
        }

        [HttpGet]
        public ActionResult ProPicChange(int id)
        {
            if (Session["UserId"] != null)
            {
                TempData["UserId"] = id;
                User user = this.repo.Get(id);
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "user");
            }




        
        }


        [HttpPost]
        public ActionResult ProPicChange(User user, HttpPostedFileBase Image)
        {

       
            //fileUpload
            String sExt = Path.GetExtension(Image.FileName).ToLower();
            String imagePath = "/Content/images/user/" + Image.FileName.Replace(sExt, "") + sExt + DateTime.Now.ToString("ddMMMyyhhmmsstt") + sExt;
            var fileName = Path.GetFileName(Image.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/images/user"), fileName);
            Image.SaveAs(path + DateTime.Now.ToString("ddMMMyyhhmmsstt") + sExt);


            user.Image = imagePath;

            Session["UserImage"] = imagePath;

            this.repo.ProfilePictureChange(user);
            return RedirectToAction("view", "Profile", new { id = user.UserID });
        }
    }
}