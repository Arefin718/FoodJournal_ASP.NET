using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Class;

namespace DataLayer
{
   public class Post
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }

        public string Title { get; set; }
        public string Image { get; set; }
      
        public string RestaurantName { get; set; }
        public string Category { get; set; }
        public int RestaurantId{ get; set; }
    
        public string Location { get; set; }
   
        public int Price { get; set; }
  
        public int Rating { get; set; }
   
        public string Time { get; set; }

        public int Status { get; set; }
        List<Report> Reports { get; set; }


        [ForeignKey("UserID")]
        public User User { get; set; }

    }
}
