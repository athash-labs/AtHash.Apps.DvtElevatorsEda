using System;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;
using AtHash.Apps.DvtElevatorsEda.Console.Services;
using AtHash.Apps.DvtElevatorsEda.Models;

AddBanner();

GetInput();

int _floors;
int _floorButtons = 2;
int _elevators;
int _elevatorButtons = _floorButtons;

var _eventBus = new InMemoryEventBus();
var _elevatorController = ElevatorControllerService.Instance;
var _elevatorDispatcherService = new ElevatorDispatcherService(_eventBus, _elevatorController.Elevators);
var _passengerService = new PassengerService(_eventBus);
var _floorButtonService = new FloorButtonService(_eventBus, _elevatorController.Elevators);

// Initialize the building, floors, elevators, etc.
_elevatorController.InitialiseBuilding(_floors, _floorButtons, _elevators, _elevatorButtons);

// Declare event handlers
var passengerElevatorRequestedHandler = new PassengerElevatorRequestedHandler(_eventBus, _floorButtonService);

// Register event handlers
await _eventBus.SubscribeAsync<PassengerElevatorRequestedEvent>(passengerElevatorRequestedHandler);
// _eventBus.RegisterHandler<PassengerRequestedElevatorEvent, PassengerRequestedElevatorHandler>(passengerRequestedElevatorHandler);

// Passenger requests an elevator
var passenger = new PassengerModel { Id = 1, Name = "John Doe" };
Console.WriteLine($"Passenger [{passenger.Id} - {passenger.Name}] requesting an elevator...");
await _passengerService.RequestElevatorAsync(
    passenger,
    _elevatorController.Floors[0],
    _elevatorController.Floors[1]);

// Wait for user input before closing the console
Console.WriteLine("Press any key to exit...");

void AddBanner()
{
    Console.WriteLine("+==============================================+");
    Console.WriteLine("| DVT Elevators - Elevator Movement Controller |");
    Console.WriteLine("+==============================================+");
    Console.WriteLine();
}

void GetInput()
{
    Console.Write("Enter the number of floors: ");
    _floors = int.Parse(Console.ReadLine());

    //Console.Write("Enter floor buttons : ");
    //_floorButtons = int.Parse(Console.ReadLine());

    Console.Write("Enter elevators per floor : ");
    _elevators = int.Parse(Console.ReadLine());

    //Console.Write("Enter elevator buttons : ");
    //_elevators = int.Parse(Console.ReadLine());
}

Console.ReadLine();
