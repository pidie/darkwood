using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Darkwood/Inventory/Object Data")]
    public class InventoryObjectData : ScriptableObject
    {
        [Tooltip("The name for the object that the player sees")]
        public string objectName;
        [Tooltip("A reference ID for this type of object")]
        public string objectID;
        [Tooltip("How many of this item can be stacked in a character's inventory")]
        [Range(1, 1000)]
        public int maxStackableAmount = 1;

        public Sprite icon;

        [Header("On Pick Up Effects")] 
        public AudioClip soundEffect;
    }
}