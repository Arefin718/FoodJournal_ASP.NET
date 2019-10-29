using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Class;

namespace DataLayer.Repository
{
    public class SubscriberRepository
    {
        private DataContext context;

        public SubscriberRepository() { this.context = new DataContext(); }



        public int Insert(Subscriber sub)
        {
            this.context.Subscribers.Add(sub);
            return this.context.SaveChanges();
        }



        public List<Subscriber> GetAll()
        {
            return this.context.Subscribers.ToList();
        }

    }
}
