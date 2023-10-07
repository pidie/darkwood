using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class InventoryItemButton : MonoBehaviour
    {
        private InventoryStorageObject _obj;
        private Image _icon;
        private Button _button;
        private TMP_Text _stackCountText;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _icon = GetComponent<Image>();
            _stackCountText = GetComponentInChildren<TMP_Text>();
        }

        public void SetButton(InventoryStorageObject obj)
        {
            _obj = obj;
            _icon.sprite = obj.data.icon;
            _stackCountText.text = obj.stackCount.ToString();
        }
    }
}