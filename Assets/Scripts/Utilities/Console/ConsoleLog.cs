using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Utilities.Console
{
    public class ConsoleLog : Singleton<ConsoleLog>
    {
        [SerializeField] private int maxLines;
        [SerializeField] private TMP_Text logArea;

        private readonly List<string> _logs = new ();

        public void AddLog(IEnumerable<string> logs)
        {
            _logs.InsertRange(0, logs);
            
            CullLogs();
            DrawLogs();
        }

        public void ClearLogs()
        {
            _logs.Clear();
            logArea.text = string.Empty;
        }

        private void CullLogs()
        {
            if (_logs.Count <= maxLines) return;
            
            _logs.RemoveRange(maxLines, _logs.Count - maxLines);
        }

        private void DrawLogs()
        {
            var logHistory = new StringBuilder(maxLines);
            
            foreach (var log in _logs)
                logHistory.Append(log + "\n");

            logArea.text = logHistory.ToString();
        }
    }
}