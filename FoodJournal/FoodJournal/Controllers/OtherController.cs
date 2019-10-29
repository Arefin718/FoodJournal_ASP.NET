using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Class;
using DataLayer.Repository;


namespace FoodJournal.Controllers
{
    public class OtherController : Controller
    {
        SubscriberRepository subRepo = new SubscriberRepository();
        // GET: Other
        public void Subscribe(Subscriber sub)
        {
            this.subRepo.Insert(sub);
        }
    }
}