using System;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace UserInterface
{
    public class InventoryItemButtonController : MonoBehaviour
    {
        [SerializeField] private InventoryItemButton buttonPrefab;
        [SerializeField] private PlayerInventoryController controller;

        private GameObject _parentUIObject;

        public static Action<List<InventoryStorageObject>> OnUpdatePlayerInventory;
        
        private void OnEnable()
        {
            if (_parentUIObject == null)
                _parentUIObject = GameObject.Find(Utilities.Globals.ui_INVENTORY_UI);
            
            if (controller == null)
                controller = GameManager.Instance.GetPlayer().GetComponent<PlayerInventoryController>();

            OnUpdatePlayerInventory += UpdateItemButtons;
            
            UpdateItemButtons(controller.inventory.GetInventory());
        }

        private void OnDisable() => OnUpdatePlayerInventory -= UpdateItemButtons;
        
        private void UpdateItemButtons(List<InventoryStorageObject> items)
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
            
            foreach (var item in items)
            {
                var button = Instantiate(buttonPrefab, transform);
                button.SetButton(item);
            }
        }
    }
}