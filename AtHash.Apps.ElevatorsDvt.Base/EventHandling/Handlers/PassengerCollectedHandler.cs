using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Handlers;

public class PassengerCollectedHandler : IEventHandler<PassengerCollectedEvent>
{
    public void Handle(PassengerCollectedEvent evt)
    {
        System.Console.WriteLine($"Elevator {evt?.Elevator?.Id} collected Passenger {evt?.Passenger?.Id} from Floor {evt?.Floor?.Id}");
        
        // Simulate item processing
        //await Task.Delay(500);
    }
}
