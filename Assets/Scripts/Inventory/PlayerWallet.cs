using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Darkwood/Player/Inventory/Wallet")]
    public class PlayerWallet : ScriptableObject
    {
        [SerializeField] private int maxValue = 99999;
        
        public int value { get; private set; }
        
        public void SetValue(int amount)
        {
            value = amount > maxValue 
                    ? maxValue 
                    : amount < 0 
                        ? 0 
                        : amount;
        }

        public void ModifyValue(int amount)
        {
            value = value + amount > maxValue 
                    ? maxValue 
                    : value + amount < 0 
                        ? 0 
                        : value + amount;
        }

        public void ClearWallet() => value = 0;
    }
}