using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities.Console
{
    public class DevConsoleController : Singleton<DevConsoleController>
    {
        [SerializeField] private ConsoleCommand[] commands;
        [SerializeField] private int maxSavedCommands = 50;
        [SerializeField] private float processCommandCooldown;

        [Header("UI")] 
        [SerializeField] private GameObject uiCanvas;
        [SerializeField] private TMP_InputField inputField;

        private List<string> previousInput { get; } = new ();
        private int _previousInputIndex;

        private float _pausedTimeScale;
        private DevConsole _console;
        private bool _canProcessCommand;        // sets a delay between command processing so user cannot spam commands

        protected override void Awake()
        {
            base.Awake();
            
            _console = new DevConsole(commands);
            inputField.caretColor = new Color(225, 225, 225);
            inputField.caretWidth = 15;
            inputField.selectionColor = new Color(30, 30, 30);

            _canProcessCommand = true;
        }

        private void OnEnable() => DevConsole.OnCommandProcessed += AddCommandToCache;

        private void OnDisable() => DevConsole.OnCommandProcessed -= AddCommandToCache;

        public void SetConsoleActive(InputAction.CallbackContext ctx)
        {
            if (!ctx.action.triggered) return;
        
            if (uiCanvas.activeSelf)
            {
                Time.timeScale = _pausedTimeScale;
                uiCanvas.SetActive(false);
            }
            else
            {
                _pausedTimeScale = Time.timeScale;
                Time.timeScale = 0;
                uiCanvas.SetActive(true);
                InitializeInputField();
            }
        }

        public void ProcessCommand()
        {
            if (!_canProcessCommand) return;
            
            _canProcessCommand = false;
            StartCoroutine(ProcessCommandCooldown());
            _console.ProcessCommand(inputField.text);
            InitializeInputField();
        }

        private IEnumerator ProcessCommandCooldown()
        {
            yield return new WaitForSeconds(processCommandCooldown);
            _canProcessCommand = true;
        }
        
        public IEnumerable<ConsoleCommand> GetPrintableCommands() => commands.Where(cmd => cmd.PrintOnHelp);

        public IEnumerable<ConsoleCommand> GetAllCommands() => commands;

        private void InitializeInputField()
        {
            inputField.text = string.Empty;
            inputField.ActivateInputField();
        }

        public void ScrollCommandInput(int direction)
        {
            if (previousInput.Count == 0) return;

            if (inputField.text == string.Empty)
            {
                inputField.text = previousInput[0];
                return;
            }

            _previousInputIndex += direction;
            
            _previousInputIndex =
                _previousInputIndex < 0
                    ? 0
                    : _previousInputIndex >= previousInput.Count
                        ? previousInput.Count - 1
                        : _previousInputIndex;

            inputField.text = previousInput[_previousInputIndex];
            inputField.caretPosition = inputField.text.Length;
        }

        private void AddCommandToCache(string userInput)
        {
            previousInput.Insert(0, userInput);
            _previousInputIndex = 0;
        }
    }
}