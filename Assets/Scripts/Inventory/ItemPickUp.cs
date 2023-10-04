using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    [RequireComponent(typeof(InventoryObject), typeof(Collider))]
    public class ItemPickUp : MonoBehaviour
    {
        [SerializeField] [Range(1, 500)] private int maxNumberOfPickUps = 1;
        [SerializeField] private bool isPersistant;
        [SerializeField] private UnityEvent onItemPickUp;
        
        private Collider _collider;
        private int _numberOfPickUpsRemaining;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;

            _numberOfPickUpsRemaining = maxNumberOfPickUps;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerManager>()) return;
            
            onItemPickUp?.Invoke();
            
            _numberOfPickUpsRemaining--;
            if (isPersistant || _numberOfPickUpsRemaining != 0) return;
            gameObject.SetActive(false);
        }
    }
}