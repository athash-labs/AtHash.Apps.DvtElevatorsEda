using System;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevationsEda.Console.Events.Handlers.Interfaces;

namespace AtHash.Apps.DvtElevationsEda.Console.Events.Handlers;

public class ElevatorButtonPressedHandler : IEventHandler<ElevatorButtonPressedEvent>
{
    public async Task HandleAsync(ElevatorButtonPressedEvent evt)
    {
        System.Console.WriteLine($"Handling: Elevator button [{evt?.ElevatorButton?.Id}] going to floor [{evt?.ElevatorButton?.DestinationFloor?.Id}]");

        //ATL: Handle the elevator button pressed event     

        await Task.CompletedTask;
    }
}
