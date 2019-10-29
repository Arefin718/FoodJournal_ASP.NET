using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repository;
using DataLayer;
using DataLayer.Class;


namespace FoodJournal.Controllers
{
    public class ForgetPasswordController : Controller
    {
        PasswordRecoveryRepository passRecoverRepo = new PasswordRecoveryRepository();
        // GET: ForgetPassword
        UserRepository repo = new UserRepository();
        
        [HttpGet]
        public ActionResult Recover()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Recover(object sender, EventArgs e,PasswordRecovery passwordRecovery)
        {
            User user = this.repo.GetByEmail(passwordRecovery.UserEmail);

            if (user!=null)
            {

                string username = user.Name;
                string password = user.Password;

                //generate random recovery code
                Random rnd = new Random();
                int recoverycode = rnd.Next(10000000, 99999999);

                MailMessage mm = new MailMessage("hdrbd1994@gmail.com", passwordRecovery.UserEmail.Trim());
                mm.Subject = "Password Recovery";
                mm.Body = string.Format("Hi {0},<br /><br />Your password reset code is {1}.<br /><br />Thank You.", username, recoverycode);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = "hdrbd1994@gmail.com";
                NetworkCred.Password = "Born1994";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);


                passwordRecovery.UserID = user.UserID;
                passwordRecovery.PasswordResetCode = recoverycode;
                passwordRecovery.PasswordResetCodeStatus = 1;
           

                this.passRecoverRepo.Insert(passwordRecovery);

                TempData["UserId"] = user.UserID;
                return RedirectToAction("PasswordRecoveryCode", "ForgetPassword");

            }
            else
            {
                TempData["EmailNotExist"] = "Your Email Doesn't exist";
                return RedirectToAction("recover", "ForgetPassword");
            }
        }

        [HttpGet]
        public ActionResult PasswordRecoveryCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordRecoveryCode(PasswordRecovery passwordrecovercode)
        {
            TempData["UserId"] = passwordrecovercode.UserID;
            PasswordRecovery passRe = this.passRecoverRepo.Get(passwordrecovercode.UserID);


            if (passwordrecovercode.PasswordResetCode == passRe.PasswordResetCode)
            {
                this.passRecoverRepo.Delete(passwordrecovercode.UserID);
                return RedirectToAction("newPassword", "ForgetPassword");
            }
            else
            {
                TempData["ResetCodeNotMatched"] = "Your Password Reset Code doesn't macth";
                return RedirectToAction("PasswordRecoveryCode", "ForgetPassword");
            }

        }


        [HttpGet]
        public ActionResult NewPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult NewPassword(User user)
        {
            User u = this.repo.ChangePassword(user);

            Session["UserID"] = u.UserID;
            Session["UserName"] = u.Name;
            Session["UserEmail"] = u.Email;
            Session["UserPass"] = u.Password;
            Session["UserLatitude"] = u.Latitude;
            Session["UserLongitude"] = u.Longitude;
            Session["UserImage"] = u.Image;

            return RedirectToAction("index", "Home");
        }


    }
}