using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Class;

namespace DataLayer.Repository
{
   public class AdminRepository
    {
        private DataContext context;

        public AdminRepository() { this.context = new DataContext(); }

        public int Insert(Admin admin)
        {
            this.context.Admins.Add(admin);
            return this.context.SaveChanges();
        }
       /* public bool EmailExist(string email)
        {


            var RegEmailId  =this.context.Admins.SingleOrDefault(d => d.Email.ToUpper()==email.ToUpper());

            bool status;
            if (RegEmailId != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }
        */

        public int Update(Admin admin)
        {
            Admin adminToUpdate = this.context.Admins.SingleOrDefault(d => d.AdminID == admin.AdminID);
            Admin temp = this.Get(admin.AdminID);
            adminToUpdate.Name = admin.Name;
            adminToUpdate.Email = admin.Email;
            adminToUpdate.Image = temp.Image;
            adminToUpdate.Password = temp.Password;

            return this.context.SaveChanges();
        }


        public int Delete(int id)
        {
            Admin adminToDelete = this.context.Admins.SingleOrDefault(d => d.AdminID == id);
            this.context.Admins.Remove(adminToDelete);
            return this.context.SaveChanges();
        }

        public Admin ChangePassword(Admin admin)
        {

            
            Admin adminToUpdate = this.context.Admins.SingleOrDefault(d => d.AdminID == admin.AdminID);
            adminToUpdate.Password = admin.Password;
            this.context.SaveChanges();

            return this.context.Admins.SingleOrDefault(d => d.AdminID == admin.AdminID);
        }
        public int ProfilePictureChange(Admin admin)
        {
            Admin adminToUpdate = this.context.Admins.SingleOrDefault(d => d.AdminID == admin.AdminID);
            adminToUpdate.Image = admin.Image;
            return this.context.SaveChanges();
        }

        public Admin Get(int id)
        {
            return this.context.Admins.SingleOrDefault(u => u.AdminID == id);

        }


        public List<Admin> GetAll()
        {
            return this.context.Admins.ToList();
        }





        public Admin GetByEmail(string email)
        {
            return this.context.Admins.SingleOrDefault(d => d.Email == email);
        }




    }
}
