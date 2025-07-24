using System;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;

public class PassengerCollectedHandler : IEventHandler<PassengerCollectedEvent>
{
    public async Task HandleAsync(PassengerCollectedEvent evt)
    {
        System.Console.WriteLine($"Elevator {evt.Elevator.Id} collected Passenger {evt.Passenger.Id} from Floor {evt.Floor.Id}");
        
        // Simulate item processing
        await Task.Delay(500);
    }
}
