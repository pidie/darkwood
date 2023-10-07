using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Console
{
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string[] commandWords;
        [SerializeField] private bool printOnHelp;
        [SerializeField] private bool printConfirmationToLog = true;
        [SerializeField] private string arguments;
        [SerializeField] [TextArea(3, 5)] private string description;
        [SerializeField] [TextArea(6, 10)] private string notes;
        [SerializeField] private UnityEvent<string[]> functionField;

        public IEnumerable<string> CommandWords => commandWords;
        public bool PrintOnHelp => printOnHelp;
        public bool PrintConfirmationToLog => printConfirmationToLog;
        public string Arguments => arguments;
        public string Description => description;
        public UnityEvent<string[]> function => functionField;

        public abstract bool Process([ItemCanBeNull] string[] args);
    }
}