using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
   public class CommentRepository: ICommentRepository
    {
        private DataContext context;

        public CommentRepository() { this.context = new DataContext(); }

        public int Insert(Comment comment)
        {
            this.context.Comment.Add(comment);
            return this.context.SaveChanges();
        }


        public Comment InsertCommentGetInsertedIdDetails(Comment comment)
        {
            this.context.Comment.Add(comment);
            this.context.SaveChanges();

            return Get(comment.CommentId);
        }

        public Comment LastInsertedCommentDetails()
        {
            int commentId = Convert.ToInt32((from t in context.Comment
                            orderby t.CommentId descending
                            select t.CommentId).First());

            return this.Get(commentId);

        }

        public int Update(Comment comment)
        {
            Comment commentToUpdate = this.context.Comment.SingleOrDefault(d => d.CommentId == comment.CommentId);

            commentToUpdate.CommentDescription = comment.CommentDescription;
     
            return this.context.SaveChanges();
        }


        public int Delete(int id)
        {
            Comment commentToDelete = this.context.Comment.SingleOrDefault(d => d.CommentId == id);
            this.context.Comment.Remove(commentToDelete);
            return this.context.SaveChanges();
        }



        public List<Comment> GetCommentByPostId(int id)
        {
            return this.context.Comment.Include("User").Where(u => u.PostId == id).ToList();

        }

        public Comment Get(int id)
        {
            return this.context.Comment.Include("User").SingleOrDefault(u => u.CommentId == id);

        }


        public List<Comment> GetAll()
        {
            return this.context.Comment.ToList();
        }

    }
}
