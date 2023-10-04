using System;
using UnityEngine;

namespace Inventory
{
    public class InventoryObject : MonoBehaviour
    {
        [SerializeField] private InventoryObjectData data;
        [Tooltip("Use to set a custom Instance ID for key objects")]
        [SerializeField] private string overrideInstanceID = string.Empty;
        
        public string InstanceID { get; private set; }

        public static Action<string> OnObjectDestroy;

        public InventoryObjectData Data => data;

        private void OnEnable()
        {
            InstanceID = overrideInstanceID == string.Empty
                ? ItemManager.GenerateNewInstanceID()
                : ItemManager.SetInstanceID(overrideInstanceID);
        }

        private void OnDestroy() => OnObjectDestroy?.Invoke(InstanceID);

        public InventoryStorageObject CreateStorageObject() => new (data);
    }
}