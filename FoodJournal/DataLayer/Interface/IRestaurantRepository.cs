using DataLayer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    interface IRestaurantRepository
    {
        List<Restaurant> GetAll();
        Restaurant Get(int id);
        int Insert(Restaurant restaurant);
        int Update(Restaurant restaurant);
        int Delete(int id);
    }
}
