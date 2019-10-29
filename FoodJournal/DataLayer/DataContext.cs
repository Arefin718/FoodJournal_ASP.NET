using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Class;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataLayer
{
   public class DataContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Restaurant> Restaurants { get;  set; }
        public DbSet<Offer> Offers { get;  set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<PasswordRecovery> PasswordRecoverys { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Report> Reports { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<FoodJournal.Models.UserModelView> UserModelViews { get; set; }
    }
}
