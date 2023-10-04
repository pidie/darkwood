using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UserInterface;

namespace Inventory
{
    public class PlayerInventoryController : MonoBehaviour
    {
        public PlayerInventory inventory;
        public PlayerWallet silverCoinWallet;
        public PlayerWallet gemstoneWallet;

        public void OnAwake()
        {
            inventory.ClearInventory();
            silverCoinWallet.ClearWallet();
            gemstoneWallet.ClearWallet();
        }

        public void AddCurrency(InventoryObjectData invData)
        {
            var data = invData as CurrencyObjectData;

            if (data == null)
                throw new NullReferenceException($"Currency must be of type {typeof(CurrencyObjectData)}");
            
            switch (data.currencyType)
            {
                case CurrencyType.SilverCoin:
                    silverCoinWallet.ModifyValue(data.value);
                    InventorySilverCoinController.OnUpdateSilverCoinCount?.Invoke();
                    break;
                case CurrencyType.Gemstone:
                    gemstoneWallet.ModifyValue(data.value);
                    break;
                case CurrencyType.MetalBar:
                    break;
                case CurrencyType.AncientCoin:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RemoveCurrency(CurrencyObjectData data)
        {
            // spend yer money!
        }

        public void AddObject(InventoryObject ob)
        {
            var item = ob.CreateStorageObject();
            if (!AddObjToStack(item))
                inventory.AddToInventory(item);

            var inventoryList = inventory.GetInventory();
            
            InventoryItemButtonController.OnUpdatePlayerInventory?.Invoke(inventoryList);
            return;

            bool AddObjToStack(InventoryStorageObject obj)
            {
                if (!CheckIfExistsInInventory(obj.id)) return false;
                
                var refs = GetAllRefsOfObj(obj.id);
                var max = obj.data.maxStackableAmount;

                foreach (var o in refs.Where(o => o.stackCount < max))
                {
                    o.stackCount++;
                    return true;
                }

                return false;
            }
        }

        public void RemoveObjectAmount(InventoryStorageObject item, int quantity = 1)
        {
            if (!CheckIfExistsInInventory(item.id)) return;

            var refs = GetAllRefsOfObj(item.id);
            foreach (var o in refs)
            {
                if (o.stackCount >= quantity)
                {
                    o.stackCount -= quantity;
                    if (o.stackCount == 0)
                        RemoveObjectStack(o);

                    return;
                }
                else
                {
                    quantity -= o.stackCount;
                    RemoveObjectStack(o);
                }
            }
        }

        public void RemoveObjectStack(InventoryStorageObject item)
        {
            if (!CheckIfExistsInInventory(item.id)) return;
            
            inventory.RemoveFromInventory(item);
        }

        private bool CheckIfExistsInInventory(string id) => inventory.GetInventory().Any(item => item.id == id);

        private List<InventoryStorageObject> GetAllRefsOfObj(string id) => inventory.GetInventory().FindAll(item => item.id == id);
    }
}