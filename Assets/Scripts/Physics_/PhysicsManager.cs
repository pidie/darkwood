using System;
using UnityEngine;
using Utilities;

namespace Physics_
{
    public class PhysicsManager : Singleton<PhysicsManager>
    {
        [SerializeField] private float gravity = Globals.GRAVITY;
    
        public float Gravity => gravity;

        public Action OnApplyPhysics;
        
        private void FixedUpdate() => OnApplyPhysics?.Invoke();
    }
}
