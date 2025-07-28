using System;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Console.Services;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;

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

    public async Task HandleAsync(ElevatorDispatchRequestedEvent evt)
    {
        // System.Console.WriteLine($"Elevator {evt.Elevator.Id} collected Passenger {evt.Passenger.Id} from Floor {evt.Floor.Id}");
        await _elevatorDispatcherService.DispatchElevatorAsync(evt?.Passenger!, evt?.CurrentFloor!, evt?.DestinationFloor!);
        System.Console.WriteLine($"Elevator requested for Passenger {evt?.Passenger?.Id} from Floor {evt?.CurrentFloor?.Id}");
    }
}
