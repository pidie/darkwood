using System;

namespace Physics_
{
    public class PhysicsManager : Utilities.Singleton<PhysicsManager>
    {
        public static Action OnApplyGravitationalForce;

        private void FixedUpdate() => OnApplyGravitationalForce.Invoke();
    }
}