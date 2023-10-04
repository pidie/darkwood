using Physics_;
using UnityEngine;

namespace Utilities
{
    public class BobInPlace : MonoBehaviour
    {
        [SerializeField] private float minHeight;
        [SerializeField] private float maxHeight = 1f;
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private PhysicsController physicsController;
        [Tooltip("Defaults the X and Z positions to 0")]
        [SerializeField] private bool useZeroPosition = true;

        private void FixedUpdate()
        {
            if (physicsController)
            {
                if (!physicsController.IsObeyGravity || physicsController.IsGrounded)
                    Bob();
            }
            else
                Bob();
        }

        private void Bob()
        {
            var heightDifference = maxHeight - (minHeight + maxHeight) / 2;
            var centerPositionY = (minHeight + maxHeight) / 2;
            
            var xPos = transform.position.x;
            var zPos = transform.position.z;
            var moveDiff = Mathf.Sin(Time.time * moveSpeed) * heightDifference + centerPositionY;
            transform.localPosition = useZeroPosition
                ? new Vector3(0, moveDiff, 0)
                : new Vector3(xPos, moveDiff, zPos);
        }
    }
}