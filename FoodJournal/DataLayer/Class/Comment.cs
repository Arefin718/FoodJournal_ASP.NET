using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace DataLayer
{
   public class Comment
    {

        public int CommentId { get; set; }

        public int UserID { get; set; }
        public string CommentDescription { get; set; }
        public int PostId { get; set; }
        public string Time { get; set; }

        [ForeignKey("UserID")]

        //[ScriptIgnore]
        public User User { get; set; }


    }
}
