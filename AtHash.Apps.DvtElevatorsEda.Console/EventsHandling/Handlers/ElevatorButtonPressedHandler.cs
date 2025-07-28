using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;

public class ElevatorButtonPressedHandler : IEventHandler<ElevatorButtonPressedEvent>
{
    public async Task HandleAsync(ElevatorButtonPressedEvent evt)
    {
        System.Console.WriteLine($"Handling: Elevator button [{evt?.ElevatorButton?.Id}] going to floor [{evt?.ElevatorButton?.DestinationFloor?.Id}]");

        //ATL: Handle the elevator button pressed event     

        await Task.CompletedTask;
    }
}
