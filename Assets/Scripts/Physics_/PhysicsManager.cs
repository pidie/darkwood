using System;
using UnityEngine;

namespace Physics_
{
    public class PhysicsManager : Utilities.Singleton<PhysicsManager>
    {
        [SerializeField] private float gravity = 9.81f;
    
        public float Gravity => gravity;

        public Action OnApplyPhysics;

        private void FixedUpdate() => OnApplyPhysics?.Invoke();
    }
}
