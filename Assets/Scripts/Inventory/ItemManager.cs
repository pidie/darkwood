using System.Collections.Generic;
using Utilities;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class ItemManager : Singleton<ItemManager>
    {
        private static readonly HashSet<string> InstanceIDs = new ();

        protected override void Awake()
        {
            base.Awake();
            InstanceIDs.Add(string.Empty);
        }

        private void OnEnable() => InventoryObject.OnObjectDestroy += RemoveInstanceID;

        private void OnDisable() => InventoryObject.OnObjectDestroy -= RemoveInstanceID;
        
        public static string GenerateNewInstanceID()
        {
            const string chars = "0123456789ABCDEF";
            var newID = string.Empty;

            while(InstanceIDs.Contains(newID))
            {
                for (var i = 0; i < 8; i++)
                    newID += chars[Random.Range(0, chars.Length)];
            }

            InstanceIDs.Add(newID);
            return newID;
        }

        public static string SetInstanceID(string id)
        {
            InstanceIDs.Add(id);
            return id;
        }

        private static void RemoveInstanceID(string instanceID) => InstanceIDs.Remove(instanceID);
    }
}
