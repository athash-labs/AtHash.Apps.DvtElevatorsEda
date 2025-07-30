using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Services;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Handlers;

public class FloorButtonElevatorRequestedHandler : IEventHandler<FloorButtonElevatorRequestedEvent>
{
    private readonly ElevatorControllerService _elevatorControllerService;
    // private readonly ElevatorDispatcherService _dispatcherService;
    
    public FloorButtonElevatorRequestedHandler(ElevatorControllerService elevatorControllerService)
    {
        _elevatorControllerService = elevatorControllerService ?? throw new ArgumentNullException(nameof(elevatorControllerService));
    }
    
    public void Handle(FloorButtonElevatorRequestedEvent evt)
    {
        // System.Console.WriteLine($"Elevator {evt.Elevator.Id} collected Passenger {evt.Passenger.Id} from Floor {evt.Floor.Id}");
        //await _elevatorControllerService.DispatchElevatorAsync(evt?.Passenger!, evt?.CurrentFloor!, evt?.DestinationFloor!);
    }
}
