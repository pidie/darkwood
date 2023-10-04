using System;
using UnityEngine;

namespace Utilities.Console.Commands
{
    [CreateAssetMenu(menuName = "Developer/New Console Command", fileName = "New Console Command")]
    public class Command : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            try
            {
                function.Invoke(args);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
        }
    }
}