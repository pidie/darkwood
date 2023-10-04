using System;

namespace Inventory
{
    [Serializable]
    public class InventoryStorageObject
    {
        public InventoryObjectData data;
        public string id;
        public int stackCount;
        
        public InventoryStorageObject(InventoryObjectData data)
        {
            this.data = data;
            id = data.objectID;
            stackCount = 1;
        }
    }
}