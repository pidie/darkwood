using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Player/Inventory/Main Inventory")]
    public class PlayerInventory : ScriptableObject
    {
        private List<InventoryStorageObject> _inventoryStorageObjects = new ();
        
        public List<InventoryStorageObject> GetInventory() => _inventoryStorageObjects;

        public void AddToInventory(InventoryStorageObject obj) => _inventoryStorageObjects.Add(obj);

        public void RemoveFromInventory(InventoryStorageObject obj) => _inventoryStorageObjects.Remove(obj);

        public void ClearInventory() => _inventoryStorageObjects.Clear();

        public void ResetInventory() => _inventoryStorageObjects = new List<InventoryStorageObject>();
    }
}