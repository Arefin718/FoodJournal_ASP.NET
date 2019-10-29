using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodJournal.Models;
using DataLayer.Repository;

namespace DataLayer
{
   public class UserRepository : IUserRepository
    {
        private DataContext context;

        public UserRepository() { this.context = new DataContext(); }

        public int Insert(User user)
        {
            this.context.Users.Add(user);
            this.context.SaveChanges();
            return user.UserID;
        }



        public int Update(User user)
        {
            User userToUpdate = this.context.Users.SingleOrDefault(d => d.UserID == user.UserID);
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.Address = user.Address;
            userToUpdate.Latitude = user.Latitude;
            userToUpdate.Longitude = user.Longitude;

            return this.context.SaveChanges();
        }

        public User ChangePassword(User user)
        {
            User userToUpdate = this.context.Users.SingleOrDefault(d => d.UserID == user.UserID);
            userToUpdate.Password = user.Password;
            this.context.SaveChanges();

            return this.context.Users.SingleOrDefault(d => d.UserID == user.UserID);
        }




        public int Delete(int id)
        {
            User userToDelete = this.context.Users.SingleOrDefault(d => d.UserID == id);
            this.context.Users.Remove(userToDelete);
            return this.context.SaveChanges();
        }

        public User Get(int id)
        {
            return this.context.Users.SingleOrDefault(d => d.UserID == id);
        }

        public User GetByEmail(string email)
        {
            return this.context.Users.SingleOrDefault(d => d.Email == email);
        }

        public List<User> GetAll()
        {
            return this.context.Users.ToList();
        }


        public int ProfilePictureChange(User user)
        {
            User userToUpdate = this.context.Users.SingleOrDefault(d => d.UserID == user.UserID);
            userToUpdate.Image = user.Image;
            return this.context.SaveChanges();
        }


        public void UserStatus(int userId)
        {
            User user = this.context.Users.SingleOrDefault(d => d.UserID == userId);
            if (user.Status == 1)
            {
                user.Status = 0;
            }
            else
            {
                user.Status = 1;
            }
            this.context.SaveChanges();

        }


        public List<UserModelView> AllUsersByAdmin()
        {
            ReportRepository reportRepo = new ReportRepository();
            RecipeRepository recipeRepo = new RecipeRepository();
            PostRepository postRepo = new PostRepository();
            RestaurantRepository restRepo = new RestaurantRepository();


           

            List<UserModelView> userModelList = new List<UserModelView>();



            foreach(var user in this.GetAll())
            {
                UserModelView umv = new UserModelView();

                umv.TotalReport = 0;
                umv.TotalRecipe = 0;
                umv.TotalPost = 0;
                umv.TotalRestaurant = 0;

                umv.Name = user.Name;
                umv.Image = user.Image;
                umv.UserId = user.UserID;

                //toral report
                foreach (var report in reportRepo.GetAllReports())
                {
                    if (user.UserID == report.ReportedId) { 

                        umv.TotalReport += 1;
                    }
                }

            //toral recipe
                foreach (var recipe in recipeRepo.GetAll())
                {
                    if (user.UserID == recipe.UserId) { 

                        umv.TotalRecipe += 1;
                     }
                }


                //toral post
                foreach (var post in postRepo.GetAll())
                {
                    if (user.UserID == post.UserID)
                    {
                        umv.TotalPost += 1;
                    }
                }



                //Total Restaurant

                foreach (var rest in restRepo.GetAll())
                {
                    if (user.UserID == rest.UserID)
                    {
                        umv.TotalRestaurant += 1;
                    }
                }

                userModelList.Add(umv);

            }



            return userModelList;
        }


        public bool EmailExists(string email)
        {
            User user = this.context.Users.SingleOrDefault(d => d.Email == email);

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
