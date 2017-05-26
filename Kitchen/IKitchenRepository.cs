using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
    public interface IKitchenRepository
    {
        Recipe GetRecipe(string name);
        List<Recipe> GetAvailableRecipes();
        void AddRecipe(Recipe newRecipe);
        
    }
}
