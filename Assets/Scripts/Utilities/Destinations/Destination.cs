using UnityEngine;

namespace Utilities.Destinations
{
    public class Destination : MonoBehaviour
    {
        [SerializeField] private string destinationID;
        [SerializeField] private string destinationName;

        public string DestinationID => destinationID;
        public string DestinationName => destinationName;

        private void Start() => DestinationManager.Instance.RegisterDestination(this);
    }
}