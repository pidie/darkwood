using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;

namespace Utilities.Console.Commands
{
    public class CommandLogic : MonoBehaviour
    {
        public void AddMoney(string[] args)
        {
            var wallet = PlayerManager.Instance.Inventory.silverCoinWallet;
            var isSet = false;

            if (args[0] == "=")
            {
                wallet.SetValue(int.Parse(args[1]));
                isSet = true;
            }
            else
                wallet.ModifyValue(int.Parse(args[0]));

            var playerGain = isSet ? "now has" : "gained";
            var amount = isSet ? args[1] : args[0];
            ConsoleLog.Instance.AddLog(new []
            {
                $"Player {playerGain} {amount} silver"
            });
        }

        public void ClearLog(string[] args)
        {
            ConsoleLog.Instance.ClearLogs();
        }

        public void PrintHelp(string[] args)
        {
            IEnumerable<ConsoleCommand> cmds;
            
            if (args.Length > 0)
            {
                cmds = DevConsoleController.Instance.GetAllCommands();
                
                foreach (var cmd in cmds)
                    if (cmd.CommandWords.Any(word => word == args[0]))
                    {
                        ConsoleLog.Instance.AddLog(new [] {FormatHelpLog(cmd)});
                        return;
                    }
            }
            else
            {
                cmds = DevConsoleController.Instance.GetPrintableCommands();
                
                foreach (var cmd in cmds)
                {
                    var str = FormatHelpLog(cmd);
                    ConsoleLog.Instance.AddLog(new[] { str + "\n" });
                }
            }
            return;
            
            string FormatHelpLog(ConsoleCommand cmd)
            {
                var str = string.Empty;
                var validWords = cmd.CommandWords
                    .Aggregate(string.Empty, (current, word) => current + $"{word} / ");

                validWords = validWords.Substring(0, validWords.Length - 3);


                str += $"{validWords} ({cmd.Arguments}) \n";
                str += cmd.Description;
                return str;
            }
        }

        public void Teleport(string[] args)
        {
            if (!CheckArgsCount(args, 1)) return;
            
            Vector3 destination;
            string destinationName;
            
            if (Destinations.DestinationManager.Instance.GetDestination(args[0], out var d))
            {
                destination = d.transform.position;
                destinationName = d.DestinationName;
            }
            else return;
            
            var playerCam = GameManager.Instance.GetPlayerCam();
            MovePlayerAndCamera();
            
            ConsoleLog.Instance.AddLog(new []
            {
                $"Player moved to {destinationName}"
            });
            
            return;
            
            void MovePlayerAndCamera()
            {
                var charController = PlayerManager.Instance.GetComponent<CharacterController>();
                var camRelativePosition = playerCam.transform.TransformPoint(charController.transform.position);

                ToggleCharacterController(charController, false);
                PlayerManager.Instance.transform.position = destination;
                ToggleCharacterController(charController, true);

                playerCam.ForceCameraPosition(camRelativePosition, Quaternion.identity);
                return;

                void ToggleCharacterController(Collider controller, bool isEnabled) =>
                    controller.enabled = isEnabled;
            }
        }

        public void ForceQuit(string[] args)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private static bool CheckArgsCount(IReadOnlyCollection<string> args, int quantity) => args.Count == quantity;
    }
}