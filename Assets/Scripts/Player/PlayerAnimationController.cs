using Physics_;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private readonly int _xVelocityHash = Animator.StringToHash("xVelocity");
        private readonly int _zVelocityHash = Animator.StringToHash("zVelocity");
        private readonly int _isPlayerHash = Animator.StringToHash("isPlayer");
        private readonly int _isPlayerMoveJumpHash = Animator.StringToHash("isPlayerMoveJump");

        private void Awake() => _animator = GetComponentInChildren<Animator>();

        private void Start() => _animator.SetBool(_isPlayerHash, true);

        private void OnEnable()
        {
            PlayerMovementController.OnPlayerMove += AnimatePlayerMove;
            PhysicsController.OnPlayerJump += AnimatePlayerJump;
        }

        private void OnDisable()
        {
            PlayerMovementController.OnPlayerMove -= AnimatePlayerMove;
            PhysicsController.OnPlayerJump -= AnimatePlayerJump;
        }

        private void AnimatePlayerMove(float xValue, float zValue)
        {
            _animator.SetFloat(_xVelocityHash, xValue);
            _animator.SetFloat(_zVelocityHash, zValue);
        }

        private void AnimatePlayerJump(bool value) => _animator.SetBool(_isPlayerMoveJumpHash, value);
    }
}