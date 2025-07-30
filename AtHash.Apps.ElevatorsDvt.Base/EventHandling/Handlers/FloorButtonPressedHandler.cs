using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Handlers;

public class FloorButtonPressedHandler : IEventHandler<FloorButtonPressedEvent>
{
    public void Handle(FloorButtonPressedEvent evt)
    {
        System.Console.WriteLine($"Handling: Floor button [{evt?.FloorButton?.Id}] on Floor: [{evt?.FloorButton?.Floor?.Id}] going [{evt?.FloorButton?.FloorButtonDirection}]");

        //ATL: Handle the floor button pressed event     

        //await Task.CompletedTask;
    }
}
