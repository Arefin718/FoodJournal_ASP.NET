using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interface;
using DataLayer.Class;

namespace DataLayer.Repository
{
    public class PasswordRecoveryRepository: IPasswordRecoveryRepository
    {
       private DataContext context;

        public PasswordRecoveryRepository() { this.context = new DataContext(); }

        public int Delete(int UserId)
        {
            PasswordRecovery recoverCodeToDelete =  this.context.PasswordRecoverys.SingleOrDefault(u=>u.UserID== UserId);
            this.context.PasswordRecoverys.Remove(recoverCodeToDelete);
            return this.context.SaveChanges();
        }

        public PasswordRecovery Get(int UserId)
        {
            return this.context.PasswordRecoverys.SingleOrDefault(d => d.UserID == UserId);
        }

        public List<PasswordRecovery> GetAll()
        {
            return this.context.PasswordRecoverys.ToList();
        }

            /*
            public int Insert(PasswordRecovery passRecover)
            {
                this.context.PasswordRecoverys.Add(passRecover);
                return this.context.SaveChanges();
            }
    */

            public int Insert(PasswordRecovery passRecover)
        {
            if (this.GetAll().Any(x => x.UserID == passRecover.UserID))
            {
                this.Update(passRecover);
            }
            else
            {
                this.context.PasswordRecoverys.Add(passRecover);
            }
        
            return this.context.SaveChanges();
        }


   
        public int Update(PasswordRecovery passRecover)
        {
            PasswordRecovery passRecoverToUpdate = this.context.PasswordRecoverys.SingleOrDefault(d => d.UserID == passRecover.UserID);
            passRecoverToUpdate.PasswordResetCodeStatus = passRecover.PasswordResetCodeStatus;
            passRecoverToUpdate.PasswordResetCode = passRecover.PasswordResetCode;
    
            return this.context.SaveChanges();
        }

    }
}
