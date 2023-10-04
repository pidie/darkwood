using UnityEngine;

namespace Utilities
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        
        private Transform _target;

        private void OnEnable() => GameManager.OnPlayerCreate += AssignTarget;

        private void OnDisable() => GameManager.OnPlayerCreate -= AssignTarget;

        private void Update()
        {
            if (_target == null) return;
    
            transform.position = _target.position + offset;
        }

        private void AssignTarget(Transform target) => _target = target;
    }
}