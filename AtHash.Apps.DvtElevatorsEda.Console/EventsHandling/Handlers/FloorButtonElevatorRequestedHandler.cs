using System;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Console.Services;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;

public class FloorButtonElevatorRequestedHandler : IEventHandler<FloorButtonElevatorRequestedEvent>
{
    private readonly ElevatorControllerService _elevatorControllerService;
    // private readonly ElevatorDispatcherService _dispatcherService;
    
    public FloorButtonElevatorRequestedHandler(ElevatorControllerService elevatorControllerService)
    {
        _elevatorControllerService = elevatorControllerService ?? throw new ArgumentNullException(nameof(elevatorControllerService));
    }
    
    public async Task HandleAsync(FloorButtonElevatorRequestedEvent evt)
    {
        // System.Console.WriteLine($"Elevator {evt.Elevator.Id} collected Passenger {evt.Passenger.Id} from Floor {evt.Floor.Id}");
        await _elevatorControllerService.DispatchElevatorAsync(evt?.Passenger!, evt?.CurrentFloor!, evt?.DestinationFloor!);
    }
}
