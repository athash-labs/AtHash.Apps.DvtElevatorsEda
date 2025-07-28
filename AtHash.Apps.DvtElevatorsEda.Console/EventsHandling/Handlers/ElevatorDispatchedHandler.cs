using System;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;

public class ElevatorDispatchedHandler : IEventHandler<ElevatorDispatchedEvent>
{
    public async Task HandleAsync(ElevatorDispatchedEvent evt)
    {
        System.Console.WriteLine($"Elevator {evt?.Elevator?.Id} dispatched to Floor {evt?.Floor?.Id}");

        // Simulate Elevator movement
        await Task.Delay(2000);
    }
}
