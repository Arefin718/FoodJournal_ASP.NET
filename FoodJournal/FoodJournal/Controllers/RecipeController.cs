using DataLayer.Class;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodJournal.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe
        private RecipeRepository repo = new RecipeRepository();

        public ActionResult showRecipe()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("login", "user");
            }
            else
            {
                int UserId = Int32.Parse(Session["UserID"].ToString());
                ViewData["UserId"] = UserId;
                return View(this.repo.GetRecipeByUserId(UserId));
            }

            
        }



        [HttpGet]
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("login", "user");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult  Create(Recipe recipe, HttpPostedFileBase Image)
        {
            String sExt = Path.GetExtension(Image.FileName).ToLower();
            int UserId = Int32.Parse(Session["UserID"].ToString());
            recipe.UserId = UserId;
            recipe.Time = System.DateTime.Now.ToString("MMMM dd, yyyy");
            
            String imagePath = "/Content/images/recipe/" + recipe.RecipeName + " " + recipe.UserId + sExt;
            string temp = recipe.RecipeName + " " + recipe.UserId + sExt;

            var path = Path.Combine(Server.MapPath("~/Content/images/recipe/"), temp);
            Image.SaveAs(path);

            recipe.Image = imagePath;
            recipe.Status = 1;

            this.repo.Insert(recipe);

            return RedirectToAction("showRecipe", "recipe");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("login", "user");
            }else
            {
                return View(this.repo.Get(id));
            }

            
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipe, HttpPostedFileBase Image)
        {


            if (recipe.Image != null)
            {
                Recipe image = repo.Get(recipe.RecipeId);
                string tmp = Path.Combine(Server.MapPath(image.Image.ToString())).ToString();
                System.IO.File.Delete(tmp);

                int UserId = Int32.Parse(Session["UserID"].ToString());
                recipe.UserId = UserId;
                String sExt = Path.GetExtension(Image.FileName).ToLower();
                String imagePath = "/Content/images/recipe/" + recipe.RecipeName + " " + recipe.UserId + sExt;
                string temp = recipe.RecipeName + " " + recipe.UserId + sExt;
                //var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/recipe/"), temp);
                Image.SaveAs(path);

                recipe.Image = imagePath;
                this.repo.Update(recipe);
                return RedirectToAction("Details", "Recipe", new { id = recipe.RecipeId });

            }
            else
            {
                Recipe image = repo.Get(recipe.RecipeId);

                string tmp = image.Image.ToString();

                recipe.Image = tmp;
                this.repo.Update(recipe);
                return RedirectToAction("Details", "Recipe", new { id = recipe.RecipeId });
            }


        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("login", "user");
            }else
            {
                return View(this.repo.Get(id));
            }
            
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            //return this.repo.Get(id).ToString();
            return View(this.repo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Recipe image = repo.Get(id);

            string tmp = Path.Combine(Server.MapPath(image.Image.ToString())).ToString();


            System.IO.File.Delete(tmp);
            this.repo.Delete(image.RecipeId);

            return RedirectToAction("showRecipe", "recipe", new { id = image.RecipeId });
        }
        
        [HttpGet]
        public ActionResult ShowAllRecipes()
        {
            return View(this.repo.GetAll());
        }
  

    }
}