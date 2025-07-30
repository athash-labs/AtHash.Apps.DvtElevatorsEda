using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Handlers;

public class ElevatorDispatchedHandler : IEventHandler<ElevatorDispatchedEvent>
{
    public void Handle(ElevatorDispatchedEvent evt)
    {
        System.Console.WriteLine($"Elevator {evt?.Elevator?.Id} dispatched to Floor {evt?.Floor?.Id}");

        // Simulate Elevator movement
        //await Task.Delay(2000);
    }
}
