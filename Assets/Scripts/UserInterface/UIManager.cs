using System;
using UnityEngine;
using Utilities;

namespace UserInterface
{
    [Serializable]
    public enum UIMenuType { Inventory, Map, QuestLog, CharacterSheet, ActionBindings }
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Transform menuParent;
        
        [Header("Menus")]
        [SerializeField] private UIMenu inventory;
        [SerializeField] private UIMenu map;
        [SerializeField] private UIMenu questLog;
        [SerializeField] private UIMenu characterSheet;
        
        public Action<UIMenuType> OnSpawnMenu;
        public Action<bool> OnTogglePlayerMenu;

        private void OnEnable()
        {
            OnSpawnMenu += SpawnMenu;
            OnTogglePlayerMenu += TogglePlayerMenu;
        }

        private void OnDisable()
        {
            OnSpawnMenu -= SpawnMenu;
            OnTogglePlayerMenu -= TogglePlayerMenu;
        }

        public void SpawnMenu(int type) => SpawnMenu((UIMenuType)type);     // called by UI button OnClick event

        private void TogglePlayerMenu(bool setActive) => menuParent.gameObject.SetActive(setActive);

        private void SpawnMenu(UIMenuType menuType)
        {
            var endThis = false;
            
            foreach (Transform child in menuParent)
            {
                if (child.GetComponent<UIMenu>().GetMenuType() == menuType)
                    endThis = true;
                Destroy(child.gameObject);
            }

            if (endThis) return;
            
            switch (menuType)
            {
                case UIMenuType.Inventory:
                    Instantiate(inventory, menuParent);
                    break;
                case UIMenuType.Map:
                    Instantiate(map, menuParent);
                    break;
                case UIMenuType.QuestLog:
                    Instantiate(questLog, menuParent);
                    break;
                case UIMenuType.CharacterSheet:
                    Instantiate(characterSheet, menuParent);
                    break;
            }
        }
    }
}
