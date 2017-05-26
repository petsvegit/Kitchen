using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Kitchen
{
    public class Recipe
    {
        public Recipe()
        {
            IngredientsAndQuantity = new List<KeyValuePair<string, double>>();
        }

        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("available")]
        public bool Available { get; set; }

        [BsonElement("ingredientsAndQuantity")]
        public List<KeyValuePair<string, double>> IngredientsAndQuantity { get; set; }

    }
}



