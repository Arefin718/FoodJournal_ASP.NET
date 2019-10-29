using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.IO;
using System.Security.Cryptography;
using System.Device.Location;
using DataLayer.Repository;

namespace FoodJournal.Controllers
{
    public class PostController : Controller
    {
        private PostRepository repo = new PostRepository();
        private CommentRepository commentRipo = new CommentRepository();
        private RestaurantRepository resRepo = new RestaurantRepository();

        // GET: Review
        [HttpGet]
        public ActionResult insert()
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
        public ActionResult insert(Post post, HttpPostedFileBase Image)
          {


                int UserId = Int32.Parse(Session["UserID"].ToString());


                //fileUpload
                String sExt = Path.GetExtension(Image.FileName).ToLower();
                String imagePath = "/Content/images/post/" + Image.FileName.Replace(sExt, "") + sExt + DateTime.Now.ToString("ddMMMyyhhmmsstt") + sExt;
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/post"), fileName);
                Image.SaveAs(path + DateTime.Now.ToString("ddMMMyyhhmmsstt") + sExt);


                post.UserID = UserId;
                 post.Status = 1;
                post.Time = DateTime.Now.ToString("MMMM dd, yyyy");
                post.Image = imagePath;

                this.repo.Insert(post);


                if (post.RestaurantId!=0)
                {
                    this.resRepo.UpdateRating(post.RestaurantId);
                }

              

                return RedirectToAction("index","Home"); 

       }



        [HttpGet]
        public ActionResult Details(int id)
        {
            //  ViewData["CommentDetails"] = this.commentRipo.GetCommentByPostIdWithUserDetails(id).ToList();

           // var sCoord = new GeoCoordinate(float.Parse(Session["UserLatitude"].ToString()), float.Parse(Session["UserLongitude"].ToString()));
            //var eCoord = new GeoCoordinate(eLatitude, eLongitude);

            //return sCoord.GetDistanceTo(eCoord);


            ViewData["CommentDetails"] = this.commentRipo.GetCommentByPostId(id);
            ViewData["Categories"] = this.repo.OccuraceForCategories();
            ViewData["RecenePosts"] = this.repo.RecentPosts(2);
            ViewData["PopularPosts"] = this.repo.PopularPosts(4);

     

            return View(this.repo.Get(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(this.repo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Post post)
        {
                this.repo.Update(post);
                return RedirectToAction("details", "post", new { id=post.PostID});
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(this.repo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.repo.Delete(id);
            return RedirectToAction("Index","Home");
        }



        [HttpGet]
        public ActionResult ShowPostsByCategory(string id)
        {
            ViewData["Category"] = id;
            return View(this.repo.GetByCategory(id));
        }

    }
}