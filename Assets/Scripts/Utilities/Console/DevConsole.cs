using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Console
{
    public class DevConsole
    {
        private readonly IEnumerable<IConsoleCommand> _commands;

        public static Action<string> OnCommandProcessed;
        
        public DevConsole(IEnumerable<IConsoleCommand> commands)
        {
            _commands = commands;
        }

        public void ProcessCommand(string inputValue)
        {
            var inputSplit = inputValue.Split(' ');

            var commandInput = inputSplit[0];
            var args = inputSplit.Skip(1).ToArray();
            
            ProcessCommand(commandInput, args, inputValue);
        }

        private void ProcessCommand(string commandInput, string[] args, string originalInput)
        {
            foreach (var command in _commands)
            {
                if (!command.CommandWords
                        .Where(commandWord => commandInput.Equals(commandWord, StringComparison.OrdinalIgnoreCase))
                        .Any(_ => command.Process(args))) 
                    continue;
                
                var cmd = command as ConsoleCommand;
                if (cmd != null && cmd.PrintConfirmationToLog)
                    ConsoleLog.Instance.AddLog(new [] {$">> {originalInput}"});
                
                OnCommandProcessed?.Invoke(originalInput);
                
                return;
            }
        }
    }
}