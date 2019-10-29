using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repository;

namespace DataLayer
{
    public class PostRepository
    {
        private DataContext context;

        private CommentRepository commenRepo = new CommentRepository();
        public PostRepository() { this.context = new DataContext(); }

        public int Insert(Post post)
        {
            this.context.Posts.Add(post);
            return this.context.SaveChanges();
        }

        public int Update(Post post)
        {
            Post postToUpdate = this.context.Posts.SingleOrDefault(d => d.PostID == post.PostID);
            postToUpdate.Description = post.Description;
            postToUpdate.RestaurantName = post.RestaurantName;
            postToUpdate.Location = post.Location;
            postToUpdate.Price = post.Price;
            postToUpdate.Title = post.Title;
            postToUpdate.Category = post.Category;

            postToUpdate.Rating = post.Rating;

            return this.context.SaveChanges();
        }


        public int Delete(int id)
        {
            Post postToDelete = this.context.Posts.SingleOrDefault(d => d.PostID == id);
            this.context.Posts.Remove(postToDelete);
            return this.context.SaveChanges();
        }


        public List<Post> GetPostByUserId(int id)
        {

            return this.context.Posts.Where(e => e.UserID == id && e.Status==1).ToList();
        }

        public Post Get(int id)
        {
            return this.context.Posts.Include("User").SingleOrDefault(d => d.PostID == id && d.Status==1);
        }

        public List<Post> GetAll()
        {
            return this.context.Posts.Where(p=>p.Status==1).ToList();
        }

        public List<Post> RecentPosts(int noOfPost)
        {
          return  this.context.Posts.OrderByDescending(p => p.PostID).Take(noOfPost).ToList();
        }


        public List<Post> PopularPosts(int noOfPost)
        {
            List<int> popPostsId= (from i in this.commenRepo.GetAll()
                                  group i.PostId by i.PostId into grp
                                  orderby grp.Count() descending
                                  select grp.Key).Take(noOfPost).ToList();


            List<Post> poularPosts = new List<Post>();

            foreach(var postId in popPostsId)
            {
                foreach (var post in this.GetAll())
                {
                    if (postId == post.PostID)
                    {
                        poularPosts.Add(post);
                    }
                }
                    
            }

            return poularPosts;

        }

        public List<string> OccuraceForCategories()
        {
          
            var categoriesInPost = this.GetAll().Select(x => x.Category).Distinct();
            List<string> allCategories = new List<string>(new string[] { "Dinner", "Lunch", "Buffet", "Desert", "BreakFast", "Beverage", "Other" });
            List<string> finalResult = new List<string>();

            foreach(var allcat in allCategories)
            {
                int totalOccurance = 0;
                foreach (var cat in this.GetAll())
                {
                    if (cat.Category == allcat)
                    {
                        totalOccurance+= 1;
                    }
                }
                finalResult.Add(allcat + " " +"(" +totalOccurance+")");
            }

            return finalResult;

        }

        public List<Post> GetByCategory(string categoryName)
        {
            return this.context.Posts.Where(p => p.Category == categoryName).ToList();
        }

        public List<Post> GetPostsByRestaurantId(int resId)
        {
            return this.context.Posts.Where(e => e.RestaurantId == resId).ToList();
        }


        public void PostStatus(int postId)
        {
            Post post = this.context.Posts.SingleOrDefault(d => d.PostID == postId);
            if (post.Status == 1)
            {
                post.Status = 0;
            }
            else
            {
                post.Status = 1;
            }
            this.context.SaveChanges();

        }


    }
}
