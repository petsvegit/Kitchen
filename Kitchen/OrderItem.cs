using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
    public class OrderItem
    {

        public OrderItem(string mealName, int quantity)
        {
            MealName = mealName;
            Quantity = quantity;
        }

        [BsonElement("mealName")]
        public string MealName { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

    }
}
