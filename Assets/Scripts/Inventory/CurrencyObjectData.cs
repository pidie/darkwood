using UnityEngine;

namespace Inventory
{
    public enum CurrencyType { SilverCoin, Gemstone, MetalBar, AncientCoin, }
    [CreateAssetMenu(menuName = "Darkwood/Inventory/Object Data/Currency")]
    public class CurrencyObjectData : InventoryObjectData
    {
        [Header("Currency Data")] 
        public CurrencyType currencyType;
        public int value;
    }
}