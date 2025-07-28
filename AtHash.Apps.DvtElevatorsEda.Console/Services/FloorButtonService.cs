using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Console.Services.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.Services;

public class FloorButtonService : IService
{
    private readonly IEventBus _eventBus;
    
    public FloorButtonService(IEventBus eventBus,
        List<ElevatorModel> elevators)
    {
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }

    public async Task PressUpButtonAsync(PassengerModel passenger,
        FloorModel currentFloor,
        FloorModel destinationFloor)
    {
        await _eventBus.PublishAsync(new FloorButtonElevatorRequestedEvent(passenger, currentFloor, destinationFloor));
    }
    
    public async Task PressDownButtonAsync(PassengerModel passenger,
        FloorModel currentFloor,
        FloorModel destinationFloor)
    {
        await _eventBus.PublishAsync(new FloorButtonElevatorRequestedEvent(passenger, currentFloor, destinationFloor));
        /*
        var availableElevators = _elevators.Where(e => e.IsOperational
            && !e.IsInMotion
            && e.Passengers.Count() < e.MaximumPassengers)
                .OrderBy(e => Math.Abs(e.CurrentFloor.Id - destinationFloor.Id));

        if (availableElevators.Count() == 0)
        {
            System.Console.WriteLine("No available elevators to dispatch");

            return;
        }

        // Select the closest available elevator
        var availableElevator = availableElevators
            .OrderBy(e => Math.Abs(e.CurrentFloor.Id - destinationFloor.Id))
            .First();
            
        availableElevator.IsInMotion = true;
        await _eventBus.PublishAsync(new ElevatorDispatchedEvent(availableElevator,
            destinationFloor));
        
        // Simulate collection process
        await Task.Delay(1500);
        
        availableElevator.IsInMotion = false;
        await _eventBus.PublishAsync(new PassengerCollectedEvent(passenger, destinationFloor, availableElevator));
        */
    }
}
