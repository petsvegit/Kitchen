using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kitchen.FridgeAPI;

namespace Kitchen

{
    public class KitchenWorker
    {

        private readonly IKitchenRepository _kitchenRepo;
        
        public KitchenWorker()
        {
            _kitchenRepo = new KitchenRepository();
        }


        public bool AddRecipe(Recipe newRecipe)
        {
            if (ValidateRecipe(newRecipe) == false) { return false; }
            _kitchenRepo.AddRecipe(newRecipe);
            return true;
        }

        private bool ValidateRecipe(Recipe newRecipe)
        {
            if (newRecipe?.Name == null) { return false; }
            return ValidateIngredientsAndQuantity(newRecipe.IngredientsAndQuantity);
        }

        private bool ValidateIngredientsAndQuantity(List<KeyValuePair<string, double>> newRecipeIngredientsAndQuantity)
        {
            if (newRecipeIngredientsAndQuantity.Count == 0) { return false; }
            return newRecipeIngredientsAndQuantity.All(item => item.Key != null);
        }

        public Recipe GetRecipe(string name)
        {
            return _kitchenRepo.GetRecipe(name);
        }

        private List<Recipe> PossibleRecipes()
        {
            List<Recipe> availableRecipes = _kitchenRepo.GetAvailableRecipes();
            List<Recipe> possibleRecipes = new List<Recipe>();

            foreach (var recipe in availableRecipes)
            {
                if (IsRecepieAvailableFromFridge(recipe)) { possibleRecipes.Add(recipe); }
            }

            return possibleRecipes;
        }

        public List<string> PossibleMeals()
        {
            List<string> mealNames = new List<string>();
            List<Recipe> possibleRecipes = PossibleRecipes();

            foreach (var recipe in possibleRecipes)
            {
                mealNames.Add(recipe.Name);
            }
            return mealNames;
        }

        private bool IsRecepieAvailableFromFridge(Recipe recipe)
        {
            return IsRecepieAvailableFromFridge(recipe, 1);
        }
                

        private bool IsRecepieAvailableFromFridge(Recipe recipe, int noOfMeals)
        {
            IFridgeAPIProxy proxy = new FridgeAPIProxy();

            foreach (var ingredientAndQuantity in recipe.IngredientsAndQuantity)
            {
                if (proxy.IsItemAvaiable(ingredientAndQuantity.Key, ingredientAndQuantity.Value * noOfMeals) == false) { return false; }
            }
            return true;
        }

        public bool PrepareMeal(string mealName, int noOfMeals)
        {
            Recipe recipe = GetRecipe(mealName);

            if (recipe == null) { return false; }
            if (recipe.Available == false) { return false; }
            if (IsRecepieAvailableFromFridge(recipe, noOfMeals) == false) { return false; }

            IFridgeAPIProxy proxy = new FridgeAPIProxy();
            double quantity;

            foreach (var ingredientAndQuantity in recipe.IngredientsAndQuantity)
            {
                quantity = ingredientAndQuantity.Value * noOfMeals;
                proxy.TakeItemFromFridge(mealName, new FridgeInventoryItemContract(mealName, quantity));
            }

            return true;
        }
    }
}

