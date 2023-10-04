using UnityEngine;

namespace Physics_
{
    /// <summary>
    /// Adds artificial gravity to gameObjects. Requires a <see cref="GroundCheck"/> reference.
    /// </summary>
    public class GravitationalForce : MonoBehaviour
    {
        [SerializeField] private GroundCheck[] groundChecks;
        [SerializeField] private float gravityMultiplier = 1f;
        [SerializeField] private bool isPlayer;
    
        private Vector3 _velocity;
        private CharacterController _characterController;
    
        private const float Gravity = -9.81f;

        public Vector3 GetVelocity => _velocity;

        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void FixedUpdate()
        {
            foreach (var groundCheck in groundChecks)
            {
                if (!groundCheck.CheckIsGrounded() || !(_velocity.y <= 0)) continue;
                
                _velocity.y = 0f;
                if (!isPlayer) SetVelocityToZero();
                    
                return;
            }

            _velocity.y += Gravity * Time.deltaTime * gravityMultiplier;

            if (_characterController)
                _characterController.Move(_velocity * Time.deltaTime);
            else
                transform.position += _velocity * Time.deltaTime;
        }

        public void Jump(float jumpForce)
        {
            foreach (var groundCheck in groundChecks)
            {
                if (groundCheck.CheckIsGrounded())
                    _velocity.y = Mathf.Sqrt(jumpForce * -2 * Gravity);
            }
        }

        public void AddForce(float force, Vector3 direction)
        {
            _velocity += force * direction;
        }

        public void SetVelocityToZero() => _velocity = Vector3.zero;
    }
}