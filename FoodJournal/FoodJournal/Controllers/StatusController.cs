using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repository;
using DataLayer;
namespace FoodJournal.Controllers
{
    public class StatusController : Controller
    {
        private RestaurantRepository resRepo = new RestaurantRepository();
        private PostRepository postRepo = new PostRepository();
        private UserRepository userRepo = new UserRepository();
        private RecipeRepository recipeRepo = new RecipeRepository();
 
        // GET: Status
        public void RestaurantStatus(int id)
        {
            this.resRepo.RestaurantStatus(id);
        }

        public void UserStatus(int id)
        {
            this.userRepo.UserStatus(id);
        }

        public void PostStatus(int id)
        {
            this.postRepo.PostStatus(id);
        }
        public void RecipeStatus(int id)
        {
            this.recipeRepo.RecipeStatus(id);
        }

        
    }
}