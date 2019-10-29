using DataLayer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
   public  interface IMenuRepository
    {
        List<Menu> GetAll();
        Menu Get(int id);
        int Insert(Menu menu);
        int Update(Menu offmenuer);
        int Delete(int id);
    }
}
