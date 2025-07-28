using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.Emunerations;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;
using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.Services;

public sealed class ElevatorControllerService
{
    private readonly IEventBus _eventBus = new InMemoryEventBus();
    private readonly ElevatorDispatcherService _elevatorDispatcherService;

    public static ElevatorControllerService Instance { get; } = Singleton.Instance;

    public List<FloorModel> Floors { get; private set; } = [];
    public List<FloorButtonModel> _floorButtons = [];
    public List<ElevatorModel> Elevators = [];
    public List<ElevatorButtonModel> _elevatorButtons = [];
    public List<PassengerModel> _passengers = [];

    private ElevatorControllerService() { }

    public void InitialiseBuilding(int floorCount,
        int floorButtonCount,
        int elevatorCount,
        int elevatorButtonCount)
    {
        // Initialize floors and elevators
        for (int i = 0; i < floorCount; i++)
        {
            var floor = new FloorModel { Id = i, Name = $"F{i}", FloorButtons = new List<FloorButtonModel>() };

        // Initialise floor buttons
        InitialiseFloorButtons(Floors);

            // Add elevators
            for (int j = 1; j <= elevatorCount; j++)
            {
                var elevator = new ElevatorModel
                {
                    Id = j,
                    Name = $"E{j}",
                    CurrentFloor = floor,
                    IsOperational = true,
                    IsInMotion = false,
                    MaximumPassengers = 10,
                    ElevatorButtons = new List<ElevatorButtonModel>()
                };

                // Elevator buttons
                for (int k = 0; k < elevatorButtonCount; k++)
                {
                    elevator.ElevatorButtons.Add(new ElevatorButtonModel
                    {
                        Id = k,
                        Name = $"EB{k}",
                        IsPressed = false
                    });
                }

                if (i == 0)
                {
                    // Set the all elevators to be on the ground floor
                    elevator.CurrentFloor = floor;
                }

                Elevators.Add(elevator);
            }

            Floors.Add(floor);
            System.Console.WriteLine($"Building initialised with {floorCount} floors and {elevatorCount} elevators");
        }
    }

    public async Task InitialiseFloorButtons(IList<FloorModel> floors)
    {
        // Initialise floor buttons
        _floorButtons.Add(new FloorButtonModel { Id = 1, Name = $"FB1", FloorButtonDirection = FloorButtonDirectionEnum.Up });
        _floorButtons.Add(new FloorButtonModel { Id = 2, Name = $"FB2", FloorButtonDirection = FloorButtonDirectionEnum.Down });
    }

    public async Task RequestElevatorDispatchAsync(PassengerModel passenger,
        FloorModel currentFloor,
        FloorModel destinationFloor)
    {
        await _eventBus.PublishAsync(new ElevatorDispatchRequestedEvent(passenger, currentFloor, destinationFloor));
        // await _elevatorDispatcherService.DispatchElevatorAsync(floor);
    }

    public async Task DispatchElevatorAsync(PassengerModel passenger,
        FloorModel currentFloor,
        FloorModel destinationFloor)
    {
        await _elevatorDispatcherService.DispatchElevatorAsync(passenger, currentFloor, destinationFloor);
    }

    private class Singleton
    {
        static Singleton() { }

        internal static readonly ElevatorControllerService Instance = new ElevatorControllerService();
    }
}
