using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Handlers;

public class ElevatorButtonPressedHandler : IEventHandler<ElevatorButtonPressedEvent>
{
    public void Handle(ElevatorButtonPressedEvent evt)
    {
        System.Console.WriteLine($"Handling: Elevator button [{evt?.ElevatorButton?.Id}] going to floor [{evt?.ElevatorButton?.DestinationFloor?.Id}]");

        //ATL: Handle the elevator button pressed event     

        //await Task.CompletedTask;
    }
}
