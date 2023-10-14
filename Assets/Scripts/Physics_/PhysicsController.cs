using System;
using UnityEngine;

namespace Physics_
{
    public class PhysicsController : MonoBehaviour
    {
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private float gravityMultiplier = 1f;
        [SerializeField] private float jumpForce;
        [SerializeField] private bool isObeyGravity = true;

        private Vector3 _velocity;
        private CharacterController _characterController;

        private const float Gravity = -9.81f;

        public bool IsGrounded => groundCheck.CheckIsGrounded();
        public bool IsObeyGravity => isObeyGravity;

        public static Action<bool> OnPlayerJump;

        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void OnEnable() => PhysicsManager.Instance.OnApplyPhysics += ApplyGravitationalForce;
        
        private void OnDisable() => PhysicsManager.Instance.OnApplyPhysics -= ApplyGravitationalForce;

        private void ApplyGravitationalForce()
        {
            if (!isObeyGravity) return;
            
            if (IsGrounded)
            {
                OnPlayerJump?.Invoke(false);
                if (_velocity.y <= 0)
                {
                    _velocity.y = -2f;
                    return;
                }
            }

            _velocity.y += Gravity * Time.deltaTime * gravityMultiplier;

            if (_characterController)
                _characterController.Move(_velocity * Time.deltaTime);
            else
                transform.position += _velocity * Time.deltaTime;
        }

        public void Jump()
        {
            if (Time.timeScale == 0 || !IsGrounded) return;
            
            _velocity.y = Mathf.Sqrt(jumpForce * -2 * Gravity);
            
            if (_characterController)
                OnPlayerJump?.Invoke(true);
        }
    }
}