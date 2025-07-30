using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Services;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Handlers;

public class PassengerElevatorRequestedHandler : IEventHandler<PassengerElevatorRequestedEvent>
{
    private readonly IEventBus _eventBus;
    private readonly FloorButtonService _floorButtonService;
    
    public PassengerElevatorRequestedHandler(IEventBus eventBus,
        FloorButtonService floorButtonService)
    {
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _floorButtonService = floorButtonService ?? throw new ArgumentNullException(nameof(floorButtonService));
    }
    
    public void Handle(PassengerElevatorRequestedEvent evt)
    {
        System.Console.WriteLine($"Handling: Passenger [{evt?.Passenger?.Id}] requested elevator on Floor: [{evt?.CurrentFloor?.Id}]; going to Floor: [{evt?.DestinationFloor?.Id}]");
        /*
        //ATL: Handle the passenger requested elevator event
        if (evt?.CurrentFloor?.Id < evt?.DestinationFloor?.Id)
            await _floorButtonService.PressUpButtonAsync(evt?.Passenger!, evt?.CurrentFloor!, evt?.DestinationFloor!);
        else
            await _floorButtonService.PressDownButtonAsync(evt?.Passenger!, evt?.CurrentFloor!, evt?.DestinationFloor!);

        // Simulate awaiting elevator process
        await Task.Delay(1500);

        await _eventBus.PublishAsync(new PassengerCollectedEvent(evt?.Passenger!, evt?.CurrentFloor!));
        await _eventBus.PublishAsync(new PassengerCollectedEvent(evt?.Passenger!, evt?.CurrentFloor!, evt?.Elevator!));
        */
    }
}
