using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen.FridgeAPI
{
    public class FridgeInventoryItemContract
    {
        public string Name { get; set; }
        public double Quantity { get; set; }

        public FridgeInventoryItemContract(string name, double quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
