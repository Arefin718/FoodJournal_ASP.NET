using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Class
{
   public  class PasswordRecovery
    {
        
        [Required]
        [Key]
        public int PasswordRecoveryId { get; set; }
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public int PasswordResetCode { get; set; }
        public int PasswordResetCodeStatus { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }


    }
}
