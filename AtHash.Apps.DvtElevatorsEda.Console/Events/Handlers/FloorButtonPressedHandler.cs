using System;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.Events.Handlers.Interfaces;

namespace AtHash.Apps.DvtElevatorsEda.Console.Events.Handlers;

public class FloorButtonPressedHandler : IEventHandler<FloorButtonPressedEvent>
{
    public async Task HandleAsync(FloorButtonPressedEvent evt)
    {
        System.Console.WriteLine($"Handling: Floor button [{evt?.FloorButton?.Id}] on Floor: [{evt?.FloorButton?.Floor?.Id}] going [{evt?.FloorButton?.FloorButtonDirection}]");

        //ATL: Handle the floor button pressed event     

        await Task.CompletedTask;
    }
}
