using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Class
{
   public class Recipe
    {
      
        [Required]
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string  CookingTime { get; set; }
        public int Person { get; set; }
        public string Time { get; set; }
        public string  Type { get; set; }
        public int Status { get; set; }


        List<Report> Reports { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
