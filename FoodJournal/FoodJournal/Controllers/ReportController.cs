using DataLayer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repository;

namespace FoodJournal.Controllers
{
    public class ReportController : Controller
    {
        private ReportRepository reportRepo = new ReportRepository();
        private RestaurantRepository restRepo = new RestaurantRepository();
        // GET: Report
        public void NewReport(Report newReport)
        {
            newReport.Time = DateTime.Now.ToString("MMMM dd, yyyy");
            this.reportRepo.Insert(newReport);
        }


        [HttpGet]
        public ActionResult ShowAllRestaurantReport()
        {
            return View(this.reportRepo.GetAll("Restaurant"));
        }


        [HttpGet]
        public ActionResult ShowAllUserReport()
        {
            return View(this.reportRepo.GetAll("User"));
        }


        [HttpGet]
        public ActionResult ShowAllPostReport()
        {
            return View(this.reportRepo.GetAll("Post"));
        }


        [HttpGet]
        public ActionResult ShowAllReciepReport()
        {
            return View(this.reportRepo.GetAll("Recipe"));
        }




    }
}