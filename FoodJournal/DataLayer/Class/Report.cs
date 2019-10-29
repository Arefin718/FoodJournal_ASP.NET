using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Class
{
   public  class Report
    {
        [Required]
        public int ReportId { get; set; }
        public string Type { get; set; }



        public int ReportedBy { get; set; }
        public int ReportedId { get; set; }
        public string Details { get; set; }
        public string Time { get; set; }
        



        [ForeignKey("ReportedBy")]
        public User ReportedByUser { get; set; }

        [ForeignKey("ReportedId")]
        public User ReportedIdUser { get; set; }






        public int? RestaurantId { get; set; }
        public int? PostId { get; set; }
        public int? RecipeId { get; set; }



        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }



    }
}
