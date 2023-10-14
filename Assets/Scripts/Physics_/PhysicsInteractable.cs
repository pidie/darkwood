using UnityEngine;

namespace Physics_
{
    public class PhysicsInteractable : MonoBehaviour, IPhysicsInteractable
    {
        [Header("Behaviors")] 
        [SerializeField] private PhysicsBehaviorData standardBehavior;
        // [SerializeField] private UnityEvent standardBehavior;
        // add additional fields for different conditional behaviors here

        private bool isGrounded => _groundCheck.CheckIsGrounded();
        public bool IsGrounded => _groundCheck;

        public Vector3 Velocity { get; private set; }

        private GroundCheck _groundCheck;
        public CharacterController characterController;

        public Vector3 SetVelocity(Vector3 value) => Velocity = value;

        private void Awake()
        {
            _groundCheck = GetComponentInChildren<GroundCheck>();
            characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            PhysicsManager.Instance.OnApplyPhysics += PhysicsBehavior;
            #if UNITY_EDITOR
            VerifyHasGroundCheck();
            #endif
        }

        private void OnDisable() => PhysicsManager.Instance.OnApplyPhysics -= PhysicsBehavior;

        private void PhysicsBehavior()
        {
            // as conditional behaviors are added, check for conditions before invoking methods
            standardBehavior.StandardBehavior?.Invoke(this, standardBehavior);
        }
        
        private void VerifyHasGroundCheck()
        {
            var groundCheck = GetComponentInChildren<GroundCheck>();

            if (groundCheck != null) return;
            
            Debug.LogError($"{gameObject.name} does not have a GroundCheck component! It has been disabled to avoid conflicts.");
            gameObject.SetActive(false);
        }
    }
}