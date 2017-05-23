using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen.FridgeAPI
{
    public interface IFridgeAPIProxy
    {
        bool IsItemAvaiable(string name, double quantity);

        void TakeItemFromFridge(string name, FridgeInventoryItemContract item);
    }
}
