using System.Collections.Generic;
using System.Linq;

namespace Utilities.Destinations
{
    public class DestinationManager : Singleton<DestinationManager>
    {
        private List<Destination> Destinations { get; } = new ();

        public bool GetDestination(string id, out Destination d)
        {
            d = null;
            
            foreach (var destination in Destinations.Where(destination => destination.DestinationID == id))
            {
                d = destination;
                return true;
            }

            return false;
        }

        public void RegisterDestination(Destination destination) => Destinations.Add(destination);
    }
}