using Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField] private Transform cameraFollow;
        [SerializeField] private Transform cameraLookAt;
        [SerializeField] private PlayerInventoryController inventory;

        public Transform GetCameraFollow => cameraFollow;
        public Transform GetCameraLookAt => cameraLookAt;
        public PlayerInventoryController Inventory => inventory;

        protected override void Awake()
        {
            base.Awake();
            inventory.OnAwake();
        }

        public void SetPlayerInput(PlayerInput playerInput)
        {
            var movementController = GetComponent<PlayerMovementController>();
            movementController.SetPlayerInput(playerInput);
        }
    }
}