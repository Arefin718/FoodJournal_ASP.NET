using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Class
{
    public class Admin
    {
        [Required]
        public int AdminID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Password { get; set; }
        
        //public List<PasswordRecovery> PasswordRecoverys { get; set; }
    }
}
