using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Services;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Handlers;

public class ElevatorDispatchRequestedHandler : IEventHandler<ElevatorDispatchRequestedEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ElevatorControllerService _elevatorControllerService;
    private readonly ElevatorDispatcherService _elevatorDispatcherService;

    public ElevatorDispatchRequestedHandler(ElevatorControllerService elevatorControllerService,
        ElevatorDispatcherService elevatorDispatcherService)
    {
        // _eventBus = elevatorControllerService?.EventBus ?? throw new ArgumentNullException(nameof(elevatorControllerService));
        _elevatorControllerService = elevatorControllerService ?? throw new ArgumentNullException(nameof(elevatorControllerService));
        _elevatorDispatcherService = elevatorDispatcherService ?? throw new ArgumentNullException(nameof(elevatorDispatcherService));
    }

    public void Handle(ElevatorDispatchRequestedEvent evt)
    {
        // System.Console.WriteLine($"Elevator {evt.Elevator.Id} collected Passenger {evt.Passenger.Id} from Floor {evt.Floor.Id}");
        _elevatorDispatcherService.DispatchElevator(evt?.Passenger!, evt?.CurrentFloor!, evt?.DestinationFloor!);
        System.Console.WriteLine($"Elevator requested for Passenger {evt?.Passenger?.Id} from Floor {evt?.CurrentFloor?.Id}");
    }
}
