using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.Models;

namespace AtHash.Apps.ElevatorsDvt.Base.Services;

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

    public void DispatchElevator(PassengerModel passenger,
        FloorModel currentFloor,
        FloorModel destinationFloor)
    {
        // Publish the event to notify that an elevator has been requested
        _eventBus.Publish(new ElevatorDispatchRequestedEvent(passenger, currentFloor, destinationFloor));

        // Logic to find and dispatch the closest available elevator
        var availableElevators = _elevators.Where(e => e.IsOperational
            && !e.IsInMotion
            && e.Passengers.Count() < e.MaximumPassengers)
                .OrderBy(e => Math.Abs(e.CurrentFloorId - destinationFloor.Id));

        if (availableElevators.Count() == 0)
        {
            System.Console.WriteLine("No available elevators to dispatch");

            return;
        }

        // Select the closest available elevator
        var availableElevator = availableElevators
            .OrderBy(e => Math.Abs(e.CurrentFloorId - destinationFloor.Id))
            .First();

        availableElevator.IsInMotion = true;
        _eventBus.Publish(new ElevatorDispatchedEvent(availableElevator,
            destinationFloor));
    }
}
