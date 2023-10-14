using UnityEngine;
using Utilities;

namespace Physics_
{
    public class PhysicsBehaviors : Singleton<PhysicsBehaviors>
    {
        public void Fall(PhysicsInteractable obj, PhysicsBehaviorData data)
        {
            if (obj.IsGrounded)
                if (obj.Velocity.y <= 0)
                {
                    obj.SetVelocity(new Vector3(0f, -2f, 0f));
                    return;
                }

            var yVelocity = PhysicsManager.Instance.Gravity * data.gravityMultiplier * Time.deltaTime;
            var velocity = obj.SetVelocity(new Vector3(obj.Velocity.x, yVelocity, obj.Velocity.z));

            if (obj.characterController)
                obj.characterController.Move(velocity * Time.deltaTime);
            else
                obj.transform.position += velocity * Time.deltaTime;
        }
    }
}