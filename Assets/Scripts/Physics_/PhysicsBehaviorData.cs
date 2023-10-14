using UnityEngine;
using UnityEngine.Events;

namespace Physics_
{
    [CreateAssetMenu(menuName = "Darkwood/Physics/Physics Behavior")]
    public class PhysicsBehaviorData : ScriptableObject
    {
        public UnityEvent<PhysicsInteractable, PhysicsBehaviorData> StandardBehavior => standardBehavior;

        [Header("Behaviors")] 
        [SerializeField] private UnityEvent<PhysicsInteractable, PhysicsBehaviorData> standardBehavior;
        [Header("Data")]
        public float gravityMultiplier = 1f;
        [Tooltip("Measured in units per second squared")]
        public float terminalVelocity;
        // add a variable to control sway - use DOTween to control sway as it falls (i.e. - a feather or a leaf falling)
    }
}