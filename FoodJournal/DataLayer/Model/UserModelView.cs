using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodJournal.Models
{
    public class UserModelView
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public int TotalRestaurant { get; set; }
        public int TotalReport { get; set; }
        public int TotalPost { get; set; }
        public int TotalRecipe { get; set; }
        public string Image { get; set; }
    }
}