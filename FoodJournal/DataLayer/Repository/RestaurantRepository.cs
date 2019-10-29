using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Class;
using DataLayer.Interface;


namespace DataLayer.Repository
{
  public  class RestaurantRepository : IRestaurantRepository
    {
        private DataContext context;
        PostRepository postRepo = new PostRepository();

        public RestaurantRepository() { this.context = new DataContext(); }

        public int Insert(Restaurant restaurant)
        {
            this.context.Restaurants.Add(restaurant);
            return this.context.SaveChanges();
        }

        public int Update(Restaurant restaurant)
        {
            Restaurant restaurantToUpdate = this.context.Restaurants.SingleOrDefault(d => d.RestaurantId == restaurant.RestaurantId);
            restaurantToUpdate.RestaurantName = restaurant.RestaurantName;
            restaurantToUpdate.Location = restaurant.Location;
            restaurantToUpdate.Latitude = restaurant.Latitude;
            restaurantToUpdate.Longitude = restaurant.Longitude;
        
            return this.context.SaveChanges();
        }


        public int Delete(int id)
        {
            Restaurant restaurantToDelete = this.context.Restaurants.SingleOrDefault(d => d.RestaurantId == id);
            this.context.Restaurants.Remove(restaurantToDelete);
            return this.context.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return this.context.Restaurants.SingleOrDefault(d => d.RestaurantId == id);
        }

           
        public List<Restaurant> GetAll()
        {
            return this.context.Restaurants.Where(r=>r.Status==1).ToList();
        }

        public List<Restaurant> GetAllByAdmin()
        {
            return this.context.Restaurants.Include("User").OrderByDescending(r=>r.Rating).ToList();
        }


        public List<Restaurant> RestaurantByMostUseReview()
        {
            List<Post> PostByReview = postRepo.GetAll().GroupBy(x => x.RestaurantId).OrderByDescending(g => g.Count()).SelectMany(g => g.Take(1)).ToList();


            List<Restaurant> sortedByReview = new List<Restaurant>();


            foreach(var item in PostByReview)
            {
                sortedByReview.Add(context.Restaurants.Include("User").SingleOrDefault(u=>u.RestaurantId==item.RestaurantId));
            }

            return sortedByReview;

        }






        public List<Restaurant> GetRestaurantsByUserId(int id)
        {
            return this.context.Restaurants.Where(u => u.UserID == id).ToList();
        }

        public void UpdateRating(int resId)
        {


       //   var idList = postRepo.GetAll().Select(x => x.RestaurantId).Distinct();
           

         

                float resrating = 0;
                int count = 0;
                foreach (var rest in this.postRepo.GetAll())
                {
                  
                    if (rest.RestaurantId == resId)
                    {
                       count++;
                       resrating += rest.Rating;
                    }

                }
              

               double finalRating =Math.Round( (resrating / count),1);
               Restaurant restaurantReview = new Restaurant();
                //restaurantReview.RestaurantId = restid;
                Restaurant restaurantRatingUpdate = this.context.Restaurants.SingleOrDefault(d => d.RestaurantId == resId);
                restaurantRatingUpdate.Rating = finalRating.ToString();
                this.context.SaveChanges();

                    resrating = 0;
                    count = 0;

        }



        public List<Restaurant> GetAllRestaurantByName(string resName)
        {
            return  this.GetAll().Where(s => s.RestaurantName.ToLower().Contains(resName)).ToList();
        }


        public List<Restaurant> GetAllRestaurantByLocation(string resLocation)
        {
            return this.GetAll().Where(s => s.Location.ToLower().Contains(resLocation)).ToList();
        }



        public void RestaurantStatus(int restaurantId)
        {
            Restaurant rest= this.context.Restaurants.SingleOrDefault(d => d.RestaurantId == restaurantId);
            if (rest.Status== 1)
            {
                rest.Status = 0;
            }
            else
            {
                rest.Status = 1;
            }
            this.context.SaveChanges();

        }

    }
}
