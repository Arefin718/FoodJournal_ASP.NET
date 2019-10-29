using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Class;

namespace DataLayer
{
    public class User
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public int Status { get; set; }


        public List<Comment> Comments { get; set; }
        public List<Post> Posts { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public List<PasswordRecovery> PasswordRecoverys { get; set; }

        public List<Report> Reports { get; set; }


    }
}
