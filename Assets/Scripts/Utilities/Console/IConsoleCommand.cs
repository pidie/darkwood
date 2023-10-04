using System.Collections.Generic;

namespace Utilities.Console
{
    public interface IConsoleCommand
    {
        IEnumerable<string> CommandWords { get; }
        bool Process(string[] args);
    }
}