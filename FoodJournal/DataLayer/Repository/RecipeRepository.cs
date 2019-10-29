using DataLayer.Class;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
   public  class RecipeRepository 

    {
        private DataContext context;



        public RecipeRepository() { this.context = new DataContext(); }


        public int Delete(int id)
        {
            Recipe recipeToDelete = this.context.Recipes.SingleOrDefault(d => d.RecipeId == id);
            this.context.Recipes.Remove(recipeToDelete);
            return this.context.SaveChanges();
        }

        public Recipe Get(int id)
        {
            return this.context.Recipes.Include("User").SingleOrDefault(d => d.RecipeId == id);
        }

      

        public int Insert(Recipe recipe)
        {
            this.context.Recipes.Add(recipe);
             return this.context.SaveChanges();
           
        }

        public int Update(Recipe recipe)
        {
            Recipe recipeToUpdate = this.context.Recipes.SingleOrDefault(d => d.RecipeId == recipe.RecipeId);
            recipeToUpdate.RecipeName = recipe.RecipeName;
            recipeToUpdate.Ingredients= recipe.Ingredients;
            recipeToUpdate.Description = recipe.Description;
            recipeToUpdate.Person = recipe.Person;
            recipeToUpdate.Type = recipe.Type;
            recipeToUpdate.Image = recipe.Image;
            recipeToUpdate.Time = recipe.Time;

            return this.context.SaveChanges();
        }

        public List<Recipe> GetAll()
        {
            List<Recipe> allRecipes = this.context.Recipes.ToList();

            List<Recipe> recipeList =  (from recp in allRecipes
                                       orderby recp.RecipeId descending
                                       where recp.Status==1
                                       select recp).ToList();

            return recipeList;
        }

        public List<Recipe> GetRecipeByUserId(int id)
        {

            return this.context.Recipes.Where(e => e.UserId == id && e.Status==1).ToList();
        }


        public void RecipeStatus(int recipeId)
        {
            Recipe recipe = this.context.Recipes.SingleOrDefault(d => d.RecipeId == recipeId);
            if (recipe.Status == 1)
            {
                recipe.Status = 0;
            }
            else
            {
                recipe.Status = 1;
            }
            this.context.SaveChanges();

        }

    }
}
