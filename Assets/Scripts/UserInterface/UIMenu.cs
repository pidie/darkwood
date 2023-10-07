using UnityEngine;

namespace UserInterface
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField] private UIMenuType menuType;

        public UIMenuType GetMenuType() => menuType;
    }
}