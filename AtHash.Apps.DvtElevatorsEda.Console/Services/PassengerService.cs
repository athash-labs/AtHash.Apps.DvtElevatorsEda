using System;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.Services;

public class PassengerService
{
    private readonly IEventBus _eventBus;

    public PassengerService(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }


    public async Task RequestElevatorAsync(PassengerModel passenger,
        FloorModel currentFloor,
        FloorModel destinationFloor)
    {
        await _eventBus.PublishAsync(new PassengerElevatorRequestedEvent(passenger, currentFloor, destinationFloor));
        // var elevatorDispatcher = new ElevatorDispatcherService(_eventBus, _elevators);
        // return elevatorDispatcher.DispatchElevatorAsync(passenger, destinationFloor);
    }

}
