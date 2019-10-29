using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Class;
using DataLayer.Repository;
using System.IO;

namespace FoodJournal.Controllers
{
    public class MenuController : Controller
    {
        
        private MenuRepository repo = new MenuRepository();
        // GET: Menu
        [HttpGet]
        public ActionResult ShowMenus(int id)
        {
            ViewData["RestaurantId"] = id;
            return View(repo.getAllMenusByRestaurantId(id));
        }

        [HttpGet]
        public ActionResult NewMenu(int id)
        {
            ViewData["RestaurantId"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult NewMenu(Menu menu, HttpPostedFileBase Image)
        {
            String sExt = Path.GetExtension(Image.FileName).ToLower();
            String imagePath = "/Content/images/restaurant/menuImage/" + menu.ItemName + " " + menu.RestaurantId+sExt;
            string temp = menu.ItemName + " " + menu.RestaurantId  + sExt;
            
            var path = Path.Combine(Server.MapPath("~/Content/images/restaurant/menuImage/"), temp);
            Image.SaveAs(path);
            menu.Image = imagePath;
            this.repo.Insert(menu);
            return RedirectToAction("ShowMenus", "menu", new { id = menu.RestaurantId });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(this.repo.GetWithRestaurantDetails(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            
            return View(this.repo.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(Menu menu, HttpPostedFileBase Image)
        {
          
           
            if (menu.Image !=null)
            {
                Menu image = repo.Get(menu.MenuId);
                String sExt1 = ".png";
                string temp1 = menu.ItemName + " " + menu.RestaurantId + sExt1;
                var currentImage = Path.Combine(Server.MapPath("~/Content/images/restaurant/menuImage/"), temp1); 

                System.IO.File.Delete(currentImage);
               
                String sExt = Path.GetExtension(Image.FileName).ToLower();
                String imagePath = "/Content/images/restaurant/menuImage/" + menu.ItemName + " " + menu.RestaurantId + sExt;
                string temp = menu.ItemName + " " + menu.RestaurantId + sExt;
                //var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/restaurant/menuImage/"), temp);
                Image.SaveAs(path);
                
                menu.Image = imagePath;
                this.repo.Update(menu);
                return RedirectToAction("Details", "menu", new { id = menu.MenuId });

            }
           else
            {
                Menu image = repo.Get(menu.MenuId);

                string tmp = image.Image.ToString();

                menu.Image = tmp;
                 this.repo.Update(menu);
                return RedirectToAction("Details", "Menu", new { id = menu.MenuId });
            }

           
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            //return this.repo.Get(id).ToString();
            return View(this.repo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(Menu menu)
        {
            Menu image = repo.Get(menu.MenuId);

            string tmp = Path.Combine(Server.MapPath(image.Image.ToString())).ToString();

           
            System.IO.File.Delete(tmp);
             this.repo.Delete(menu.MenuId);
           
            return RedirectToAction("ShowMenus", "menu", new { id = menu.RestaurantId });
        }


    }
}