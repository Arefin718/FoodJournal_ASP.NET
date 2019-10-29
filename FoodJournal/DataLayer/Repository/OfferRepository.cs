using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interface;
using DataLayer.Class;

namespace DataLayer.Repository
{
   public class OfferRepository:IOffer
    {
        private DataContext context;

        public OfferRepository() { this.context = new DataContext(); }

        public int Insert(Offer offer)
        {
            this.context.Offers.Add(offer);
            return this.context.SaveChanges();
        }

        public int Update(Offer offer)
        {
            Offer offerToUpdate = this.context.Offers.SingleOrDefault(d => d.OfferId == offer.OfferId);
            offerToUpdate.ItemName = offer.ItemName;
            offerToUpdate.OfferDescription = offer.OfferDescription;
            offerToUpdate.Price = offer.Price;
            offerToUpdate.OfferValidity = offer.OfferValidity;
            return this.context.SaveChanges();
        }


        public int Delete(int id)
        {
            Offer offerToDelete = this.context.Offers.SingleOrDefault(d => d.OfferId == id);
            this.context.Offers.Remove(offerToDelete);
            return this.context.SaveChanges();
        }

        public Offer Get(int id)
        {
            return this.context.Offers.SingleOrDefault(d => d.OfferId == id);
        }

        public Offer GetWithRestaurantDetails(int id)
        {
            return this.context.Offers.Include("Restaurant").SingleOrDefault(d => d.OfferId == id);
        }

        public List<Offer> GetAll()
        {
            return this.context.Offers.Include("Restaurant").ToList();
        }


        public List<Offer> getAllOffersByRestaurantId(int id)
        {
            return this.context.Offers.Include("Restaurant").Where(u=> u.RestaurantId==id).ToList();
        }




    }
}
