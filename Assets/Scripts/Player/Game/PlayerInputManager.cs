using System.Collections.Generic;
using System.Linq;
using Physics_;
using UnityEngine;
using UnityEngine.InputSystem;
using UserInterface;
using Utilities.Console;
using static Utilities.Globals;

namespace Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        public static PlayerInputActions Controls { get; private set; }

        private InputAction _move;
        private InputAction _turn;
        private InputAction _run;
        private InputAction _jump;
        private InputAction _rotateCamera;
        private InputAction _crouch;
        private InputAction _menuInventory;
        private InputAction _menuActionBindings;
        private InputAction _menuQuestLog;
        private InputAction _menuCharacterSheet;
        private InputAction _menuMap;
        private InputAction _menuSettings;
        private InputAction _menuMain;
        private InputAction _toggleToConsole;
        private InputAction _toggleFromConsole;
        private InputAction _consoleSubmit;
        private InputAction _scrollCommandsUp;
        private InputAction _scrollCommandsDown;

        private PlayerMovementController _playerMovementController;
        private PhysicsController _physicsController;

        private List<InputActionMap> _controlsMaps = new ();
        private List<InputActionMap> _cachedMaps = new ();

        private void Awake()
        {
            Controls = new PlayerInputActions();

            _playerMovementController = GetComponent<PlayerMovementController>();
            _physicsController = GetComponent<PhysicsController>();
        }

        private void OnEnable()
        {
            _move = Controls.General.Movement;
            _move.performed += ctx => _playerMovementController.MovementDirection = ctx.ReadValue<Vector2>();
            _move.canceled += ctx => _playerMovementController.MovementDirection = ctx.ReadValue<Vector2>();

            _turn = Controls.General.Turn;
            
            _run = Controls.General.Run;
            _run.started += OnRun;
            _run.canceled += OnRun;

            _jump = Controls.General.Jump;
            _jump.started += ctx => _physicsController.Jump();

            _rotateCamera = Controls.General.RotateCamera;
            _rotateCamera.started += ctx => OnRotateCamera(ctx);
            _rotateCamera.canceled += ctx => OnRotateCamera(ctx);

            _menuInventory = Controls.General.Inventory;
            _menuInventory.started += ctx => OnMenuSpawn(ctx, UIMenuType.Inventory);
            _menuInventory.canceled += ctx => OnMenuSpawn(ctx, UIMenuType.Inventory);

            _menuActionBindings = Controls.General.ActionBindings;
            _menuActionBindings.started += ctx => OnMenuSpawn(ctx, UIMenuType.ActionBindings);
            _menuActionBindings.canceled += ctx => OnMenuSpawn(ctx, UIMenuType.ActionBindings);

            _menuQuestLog = Controls.General.QuestLog;
            _menuQuestLog.started += ctx => OnMenuSpawn(ctx, UIMenuType.QuestLog);
            _menuQuestLog.canceled += ctx => OnMenuSpawn(ctx, UIMenuType.QuestLog);

            _menuCharacterSheet = Controls.General.CharacterSheet;
            _menuCharacterSheet.started += ctx => OnMenuSpawn(ctx, UIMenuType.CharacterSheet);
            _menuCharacterSheet.canceled += ctx => OnMenuSpawn(ctx, UIMenuType.CharacterSheet);

            _menuMap = Controls.General.Map;
            _menuMap.started += ctx => OnMenuSpawn(ctx, UIMenuType.Map);
            _menuMap.canceled += ctx => OnMenuSpawn(ctx, UIMenuType.Map);
            
            _toggleToConsole = Controls.General.ToggleConsole;
            _toggleToConsole.started += ctx => OnSwitchActionMap(ctx, Controls.DeveloperConsole);

            _toggleFromConsole = Controls.DeveloperConsole.ToggleConsole;
            _toggleFromConsole.started += ctx => OnSwitchActionMap(ctx, cacheActiveMaps: false, enableCachedMaps: true);

            _consoleSubmit = Controls.DeveloperConsole.Submit;
            _consoleSubmit.started += ctx => DevConsoleController.Instance.ProcessCommand();

            _scrollCommandsUp = Controls.DeveloperConsole.ScrollCommandsUp;
            _scrollCommandsUp.started += ctx => DevConsoleController.Instance.ScrollCommandInput(1);
            
            _scrollCommandsDown = Controls.DeveloperConsole.ScrollCommandsDown;
            _scrollCommandsDown.started += ctx => DevConsoleController.Instance.ScrollCommandInput(-1);
            
            Controls.General.Enable();
        }

        private void OnDisable()
        {
            Controls.General.Disable();
            Controls.DeveloperConsole.Disable();
        }

        private void Start()
        {
            _controlsMaps.Add(Controls.General.Get());
            _controlsMaps.Add(Controls.DeveloperConsole.Get());
        }

        private void OnRun(InputAction.CallbackContext ctx)
        {
            var scheme = _playerMovementController.GetCurrentControlScheme();
        
            _playerMovementController.IsRunning = scheme switch
            {
                inp_GAMEPAD when ctx.started => !_playerMovementController.IsRunning,
                inp_KEYBOARD => ctx.started,
                inp_KEYBOARD_SOUTHPAW => ctx.started,
                _ => _playerMovementController.IsRunning
            };
        }

        private void OnRotateCamera(InputAction.CallbackContext ctx)
        {
            _playerMovementController.SetIsPlayerTurning(ctx.canceled);
            // player follow camera. recenter to target heading.enabled
        }

        private void OnMenuSpawn(InputAction.CallbackContext ctx, UIMenuType menuType)
        {
            if (ctx.started)
            {
                UIManager.Instance.OnSpawnMenu?.Invoke(menuType);
                if (menuType == UIMenuType.Map) MapCameraController.OnMapActive?.Invoke(true);
            }
            
            // if the Hold Menu button is toggled, the menu will only be active while the button is being held.
            if (GameSettings.GetHoldMenu(menuType) && ctx.canceled)
                UIManager.Instance.OnSpawnMenu?.Invoke(menuType);

            if (ctx.canceled && menuType == UIMenuType.Map) MapCameraController.OnMapActive?.Invoke(false);
        }

        private void OnSwitchActionMap(InputAction.CallbackContext ctx, InputActionMap actionMap = null, bool cacheActiveMaps = true, bool enableCachedMaps = false)
        {
            if (actionMap is { enabled: true }) return;

            UIManager.Instance.OnTogglePlayerMenu?.Invoke(actionMap != Controls.DeveloperConsole.Get());

            if (cacheActiveMaps)
            {
                _cachedMaps.Clear();

                foreach (var map in _controlsMaps.Where(map => map.enabled))
                    _cachedMaps.Add(map);
            }

            Controls.Disable();
            
            if (enableCachedMaps)
                foreach (var map in _cachedMaps)
                    map.Enable();

            actionMap?.Enable();
        }
    }
}