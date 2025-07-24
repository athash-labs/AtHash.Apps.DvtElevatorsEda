using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.Services;

public class ElevatorDispatcherService
{
    private readonly IEventBus _eventBus;
    private readonly List<ElevatorModel> _elevators;
    
    public ElevatorDispatcherService(IEventBus eventBus,
        List<ElevatorModel> elevators)
    {
        _eventBus = eventBus;
        _elevators = elevators;
    }
    
    public async Task DispatchElevatorAsync(PassengerModel passenger,
        FloorModel destinationFloor)
    {
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
    }
}
