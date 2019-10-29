using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer.Class
{
  public  class Restaurant
    {
        [Required]
        public int RestaurantId { get; set; }
        public int UserID { get; set; }
        public string RestaurantName { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Rating { get; set; }
        public int Status { get; set; }


        List<Offer> Offers { get; set; }
        List<Menu> Menus { get; set; }
        List<Report> Reports { get; set; }



        [ForeignKey("UserID")]
        public User User { get; set; }

    }
}
