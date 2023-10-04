using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private UnityEvent raisingEvent;

        private void OnEnable() => gameEvent.AddListener(this);

        private void OnDisable() => gameEvent.RemoveListener(this);

        public void OnEventRaised() => raisingEvent?.Invoke();
    }
}