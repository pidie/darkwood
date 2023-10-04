using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class InventorySilverCoinController : MonoBehaviour
    {
        [SerializeField] private TMP_Text textObject;
        [SerializeField] private PlayerInventoryController controller;
        
        public static Action OnUpdateSilverCoinCount;

        private void OnEnable()
        {
            OnUpdateSilverCoinCount += UpdateText;
            OnUpdateSilverCoinCount?.Invoke();
        }

        private void OnDisable() => OnUpdateSilverCoinCount -= UpdateText;

        private void UpdateText() => textObject.text = controller.silverCoinWallet.value.ToString();
    }
}