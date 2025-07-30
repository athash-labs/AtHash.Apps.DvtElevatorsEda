using AtHash.Apps.ElevatorsDvt.Base.Controllers.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Enumerations;
using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Models;
using System.Runtime.CompilerServices;

namespace AtHash.Apps.ElevatorsDvt.Base.Controllers
{
    public class ElevatorController : IElevatorController
    {
        private readonly IEventBus _eventBus;
        private readonly List<ElevatorModel> _elevators;
        private readonly int _totalFloors = 35;
        private readonly int _elevatorsPerFloor = 5;

        public ElevatorController(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _elevators = Enumerable.Range(0, _elevatorsPerFloor)
                .Select(_ => new ElevatorModel())
                .ToList();

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _eventBus.Subscribe<ElevatorRequestedEvent>(HandleElevatorRequest);
            _eventBus.Subscribe<InsideButtonPressedEvent>(HandleInsideButtonPress);
        }

        public void HandleElevatorRequest(ElevatorRequestedEvent request)
        {
            // Find the nearest available elevator
            var elevator = FindNearestElevator(request.FloorNumber, request.ElevatorDirection);

            if (elevator != null)
            {
                Console.WriteLine($"Dispatching elevator {elevator.Id} to floor {request.FloorNumber}");

                // Simulate elevator movement
                MoveElevatorToFloor(elevator, request.FloorNumber);

                // Notify that elevator has arrived
                _eventBus.Publish(new ElevatorArrivedEvent
                {
                    FloorNumber = request.FloorNumber,
                    ElevatorId = elevator.Id
                });
            }
        }

        public void HandleInsideButtonPress(InsideButtonPressedEvent request)
        {
            var elevator = _elevators.FirstOrDefault(e => e.Id == request.ElevatorId);
            if (elevator != null)
            {
                elevator.RequestedFloors.Add(request.DestinationFloor);
                elevator.CurrentDirection = request.DestinationFloorId > elevator.CurrentFloorId
                    ? ElevatorDirection.GoingUp
                    : ElevatorDirection.GoingDown;

                MoveElevatorToFloor(elevator, request.DestinationFloorId);

                _eventBus.Publish(new ElevatorArrivedEvent
                {
                    FloorNumber = request.DestinationFloorId,
                    //Floor = request.DestinationFloor,
                    ElevatorId = elevator.Id
                });
            }
        }

        private ElevatorModel FindNearestElevator(int floorNumber, ElevatorDirection direction)
        {
            // Simple algorithm to find the nearest available elevator
            return _elevators
                .Where(e => e.Status == ElevatorStatus.Idle
                    || (e.CurrentDirection == direction
                    && ((direction == ElevatorDirection.GoingUp && e.CurrentFloorId <= floorNumber)
                    || (direction == ElevatorDirection.GoingDown && e.CurrentFloorId >= floorNumber))))
                .OrderBy(e => Math.Abs(e.CurrentFloorId - floorNumber))
                .FirstOrDefault();
        }

        private void MoveElevatorToFloor(ElevatorModel elevator, int targetFloor)
        {
            elevator.Status = targetFloor > elevator.CurrentFloorId
                ? ElevatorStatus.MovingUp
                : ElevatorStatus.MovingDown;

            // Simulate movement between floors
            int step = targetFloor > elevator.CurrentFloorId ? 1 : -1;

            while (elevator.CurrentFloorId != targetFloor)
            {
                Thread.Sleep(500); // Simulate time between floors
                elevator.CurrentFloorId += step;
                Console.WriteLine($"Elevator {elevator.Id} is now at floor {elevator.CurrentFloorId}");
            }

            elevator.Status = ElevatorStatus.DoorsOpen;
            Console.WriteLine($"Elevator {elevator.Id} has arrived at floor {targetFloor}. Doors are open.");

            // Simulate doors closing after a delay
            Thread.Sleep(2000);
            elevator.Status = ElevatorStatus.Idle;
            Console.WriteLine($"Elevator {elevator.Id} doors are now closed.");
        }
    }
}
