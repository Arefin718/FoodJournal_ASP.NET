using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Class;

namespace DataLayer.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private DataContext context;

        public MenuRepository() { this.context = new DataContext(); }
        public int Delete(int id)
        {
            Menu menuToDelete = this.context.Menus.SingleOrDefault(d => d.MenuId == id);
            this.context.Menus.Remove(menuToDelete);
            return this.context.SaveChanges();
        }

        public Menu Get(int id)
        {
            return this.context.Menus.SingleOrDefault(d => d.MenuId == id);
        }

        public List<Menu> GetAll()
        {
            return this.context.Menus.Include("Restaurant").ToList();
        }

        public int Insert(Menu menu)
        {
            this.context.Menus.Add(menu);
            return this.context.SaveChanges();
        }

        public int Update(Menu menu)
        {
            Menu menuToUpdate = this.context.Menus.SingleOrDefault(d => d.MenuId == menu.MenuId);
            menuToUpdate.ItemName = menu.ItemName;
            menuToUpdate.MenuDescription = menu.MenuDescription;
            menuToUpdate.Image = menu.Image;
            menuToUpdate.Price = menu.Price;

            return this.context.SaveChanges();
        }

        public Menu GetWithRestaurantDetails(int id)
        {
            return this.context.Menus.Include("Restaurant").SingleOrDefault(d => d.MenuId == id);
        }
        public List<Menu> getAllMenusByRestaurantId(int id)
        {
            return this.context.Menus.Include("Restaurant").Where(u => u.RestaurantId == id).ToList();
        }
    }
}
