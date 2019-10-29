using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Class
{
   public  class Offer
    {
        [Required]
        public int OfferId { get; set; }
        public int RestaurantId { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string OfferDescription { get; set; }
        public string OfferValidity { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }
    }
}
