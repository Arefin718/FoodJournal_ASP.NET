using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Class;
using DataLayer.Repository;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace FoodJournal.Controllers
{
    public class OfferController : Controller
    {
        private OfferRepository repo = new OfferRepository();
        private SubscriberRepository subRepo = new SubscriberRepository();

        [HttpGet]
        public ActionResult ShowOffers(int id)
        {
            ViewData["RestaurantId"] = id;
            return View(this.repo.getAllOffersByRestaurantId(id));
        }


        [HttpGet]
        public ActionResult NewOffer(int id)
        {
            ViewData["RestaurantId"] = id;
            return View();
        }


        [HttpPost]
        public ActionResult NewOffer(Offer offer, HttpPostedFileBase Image)
        {
            String sExt = Path.GetExtension(Image.FileName).ToLower();
            String imagePath = "/Content/images/restaurant/offer/" + Image.FileName.Replace(sExt, "") + sExt + DateTime.Now.ToString("ddMMMyyhhmmsstt") + sExt;
            var fileName = Path.GetFileName(Image.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/images/restaurant/offer/"), fileName);
            Image.SaveAs(path + DateTime.Now.ToString("ddMMMyyhhmmsstt") + sExt);

            offer.Image = imagePath;
            this.repo.Insert(offer);

            this.InformSubscriber(offer.OfferId);

            return RedirectToAction("ShowOffers", "Offer", new { id = offer.RestaurantId });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            //   Offer offer = this.repo.GetWithRestaurantDetails(id);

            //var userLocation = new GeoCoordinate(double.Parse(Session["UserLatitude"].ToString()), double.Parse(Session["UserLongitude"].ToString()));

            //var resLocation = new GeoCoordinate(double.Parse(offer.Restaurant.Latitude.ToString()), double.Parse(offer.Restaurant.Longitude.ToString()));

            //double distenceFromUser = userLocation.GetDistanceTo(resLocation);

            //TempData["distenceFromUser"] = Math.Round(Convert.ToDouble(distenceFromUser/1000),2)+" km";

            return View(this.repo.GetWithRestaurantDetails(id));
        }


        public ActionResult Edit(int id)
        {
            return View(this.repo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Offer offer)
        {
            this.repo.Update(offer);
            return RedirectToAction("Details", "Offer", new { id = offer.OfferId });
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(this.repo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(Offer offer)
        {
            this.repo.Delete(offer.OfferId);
            return RedirectToAction("ShowOffers", "offer", new { id = offer.RestaurantId });
        }

        [HttpGet]
        public ActionResult ShowAllOffers()
        {
            return View(this.repo.GetAll());
        }


        public void InformSubscriber(int offerId)
        {


            Offer offer = repo.GetWithRestaurantDetails(offerId);


        foreach (var item in this.subRepo.GetAll())
            {
                
                MailMessage mm = new MailMessage("hdrbd1994@gmail.com", item.SubscriberMail.Trim());
                mm.Subject = "New Offer Posted";
                mm.Body = string.Format("Hi ,<br /><br />New Offer given by {0} Restaurant.<br />Item Name:{1}<br />Price:{2}<br />For More Info click <a href='http://localhost:65526/offer/Details/{3}'><a/>{3}<br />Thank You.", offer.Restaurant.RestaurantName,offer.ItemName,offer.Price,offer.OfferId);
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
            }
    

        }


    }
}