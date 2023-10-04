using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float baseSpeed = 5f;
        [SerializeField] private float runSpeedMultiplier = 1.5f;
        [SerializeField] private float turnSpeedMultiplier = 3f;
        [SerializeField] private float movementForcedDeadZone = 0.8f;
        
        private Transform _cameraTransform;
        private CharacterController _characterController;
        private PlayerInput _playerInput;
        private bool _isPlayerTurning = true;
        
        public Vector3 MovementDirection { get; set; }
        public bool IsRunning { get; set; }

        public static Action<float, float> OnPlayerMove;

        public string GetCurrentControlScheme() => _playerInput.currentControlScheme;

        public void SetIsPlayerTurning(bool value) => _isPlayerTurning = value;

        public void SetPlayerInput(PlayerInput playerInput) => _playerInput = playerInput;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            if (Camera.main != null) _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (Time.timeScale == 0) return;
            
            var moveVector = DetermineMovementVector();

            if (!CheckIfShouldMove(moveVector)) return;

            var movementSpeed = DetermineMovementSpeed();
            _characterController.Move(moveVector * (movementSpeed * Time.deltaTime));

            AnimateMovement();
            RotateCharacter();
        }

        private Vector3 DetermineMovementVector()
        {
            var move = _cameraTransform.forward * MovementDirection.y + _cameraTransform.right * MovementDirection.x;
            move = new Vector3(move.x, 0f, move.z);

            return move;
        }

        private bool CheckIfShouldMove(Vector3 moveVector)
        {
            if (!(moveVector.magnitude < movementForcedDeadZone)) return true;
            
            OnPlayerMove?.Invoke(0f, 0f);
            if (GetCurrentControlScheme() == Utilities.Globals.inp_GAMEPAD)
                IsRunning = false;
            return false;
        }

        private float DetermineMovementSpeed()
        {
            var movementSpeed = baseSpeed;
            if (IsRunning) 
                movementSpeed *= runSpeedMultiplier;

            return movementSpeed;
        }

        private void AnimateMovement()
        {
            if (IsRunning)
                OnPlayerMove?.Invoke(MovementDirection.x * 3f, MovementDirection.y * 3f);
            else
                OnPlayerMove?.Invoke(MovementDirection.x, MovementDirection.y);
        }
        
        private void RotateCharacter()
        {
            if (!_isPlayerTurning) return;
            var targetRotation = Quaternion.Euler(0f, _cameraTransform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Lerp(Quaternion.Inverse(transform.rotation) * _cameraTransform.rotation, targetRotation,
                Time.deltaTime * turnSpeedMultiplier);
        }
    }
}   