using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace FoodJournal.Controllers
{
    public class CommentController : Controller
    {
        private CommentRepository repo = new CommentRepository();
        // GET: Comment


    /*
        public void CommentSubmit(Comment comment)
        {
            this.repo.InsertCommentGetInsertedId(comment);
        }
        */

        public JsonResult CommentSubmit(Comment commentp)
        {

            Comment comment = this.repo.InsertCommentGetInsertedIdDetails(commentp);


            return Json(

                new
                {
                    comment =

                     new
                     {
                         Id = comment.CommentId,
                         CommentDescription = comment.CommentDescription,
                         UserId = comment.User.UserID,
                         Time = comment.Time,
                         Name = comment.User.Name,
                         Image = comment.User.Image
                     }
                },


                JsonRequestBehavior.AllowGet);



        }

      /*  public JsonResult LastInsertedCommentDetails()
        {
            Comment comment = this.repo.LastInsertedCommentDetails();



            return Json(




                new
                {
                    comment = 

                     new {Id = comment.CommentId,
                         CommentDescription = comment.CommentDescription,
                         UserId = comment.User.UserID,
                         Time = comment.Time,
                         Name = comment.User.Name
                        }                           
                }, 
                

                JsonRequestBehavior.AllowGet);
        }
        */

    }
}