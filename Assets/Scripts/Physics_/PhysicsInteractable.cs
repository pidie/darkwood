using UnityEngine;
using UnityEngine.Events;

namespace Physics_
{
    public class PhysicsInteractable : MonoBehaviour, IPhysicsInteractable
    {
        [Header("Behaviors")]
        [SerializeField] private UnityEvent standardBehavior;

        // add additional fields for different conditional behaviors here

        private float _gravity;

        private void Awake() => _gravity = PhysicsManager.Instance.Gravity;

        private void OnEnable() => PhysicsManager.Instance.OnApplyPhysics += PhysicsBehavior;

        private void OnDisable() => PhysicsManager.Instance.OnApplyPhysics -= PhysicsBehavior;

        private void PhysicsBehavior()
        {
            // as conditional behaviors are added, check for conditions before invoking methods
            standardBehavior?.Invoke();
        }
    }
}