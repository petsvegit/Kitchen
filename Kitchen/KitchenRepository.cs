using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
    class KitchenRepository : IKitchenRepository
    {
        public void AddRecipe(Recipe newRecipe)
        {
            var collection = GetMongoConnection().GetCollection<Recipe>("Recipe");
            collection.InsertOne(newRecipe);

        }

        public List<Recipe> GetAvailableRecipes()
        {
            var collection = GetMongoConnection().GetCollection<Recipe>("Recipe");

            return (from recipe in collection.AsQueryable<Recipe>()
                    where recipe.Available == true
                    select recipe).ToList();

        }

        public Recipe GetRecipe(string name)
        {
            var collection = GetMongoConnection().GetCollection<Recipe>("Recipe");

            return (from recipe in collection.AsQueryable<Recipe>()
                    where recipe.Name == name
                    select recipe).FirstOrDefault();

        }

        private IMongoDatabase GetMongoConnection()
        {
            string connectionString = "mongodb://localhost:49155";

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));

            //1. Connect to MongoDB instance running on localhost/IP
            var client = new MongoClient(settings);

            //Access database named 'KitchenDb'
            return client.GetDatabase("KitchenDb");
        }

    }
}
