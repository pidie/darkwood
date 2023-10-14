using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    [CreateAssetMenu(menuName = "Darkwood/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _listeners = new();

        public void AddListener(GameEventListener listener) => _listeners.Add(listener);

        public void RemoveListener(GameEventListener listener) => _listeners.Remove(listener);

        public void RaiseEvent()
        {
            foreach (var listener in _listeners)
                listener.OnEventRaised();
        }
    }
}