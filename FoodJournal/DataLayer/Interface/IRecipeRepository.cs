using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
   public interface IRecipeRepository
    {
        List<Post> GetAll();
        User Get(int id);
        int Insert(Post user);
        int Update(Post user);
        int Delete(int id);
    }
}
