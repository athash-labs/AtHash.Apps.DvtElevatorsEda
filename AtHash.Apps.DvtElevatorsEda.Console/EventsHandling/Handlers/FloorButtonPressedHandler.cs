using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;

public class FloorButtonPressedHandler : IEventHandler<FloorButtonPressedEvent>
{
    public async Task HandleAsync(FloorButtonPressedEvent evt)
    {
        System.Console.WriteLine($"Handling: Floor button [{evt?.FloorButton?.Id}] on Floor: [{evt?.FloorButton?.Floor?.Id}] going [{evt?.FloorButton?.FloorButtonDirection}]");

        //ATL: Handle the floor button pressed event     

        await Task.CompletedTask;
    }
}
