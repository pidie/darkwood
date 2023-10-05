using System;

namespace Physics_
{
    public class PhysicsManager : Utilities.Singleton<PhysicsManager>
    {
        [SerializeField] private float gravity = 9.81f;
    
        public float Gravity { get; } => gravity;

        public Action OnApplyPhysics;

        private void FixedUpdate() => OnApplyPhysics?.Invoke();

        // public static Action OnApplyGravitationalForce;

        // private void FixedUpdate() => OnApplyGravitationalForce?.Invoke();
    }
}
