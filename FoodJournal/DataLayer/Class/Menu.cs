using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Class
{
   public class Menu
    {
        [Required]
        public int MenuId { get; set; }
        public int RestaurantId { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string MenuDescription { get; set; }
        
        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }
    }
}
