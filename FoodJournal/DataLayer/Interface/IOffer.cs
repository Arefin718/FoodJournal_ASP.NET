using DataLayer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    interface IOffer
    {
        List<Offer> GetAll();
        Offer Get(int id);
        int Insert(Offer offer);
        int Update(Offer offer);
        int Delete(int id);
    }
}
