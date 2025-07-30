using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;

namespace AtHash.Apps.ElevatorsDvt.Base.EventHandling.Handlers
{
    internal class InsideButtonPressHandler
    {
        public void Handle(PassengerCollectedEvent evt)
        {
            System.Console.WriteLine($"Elevator {evt?.Elevator?.Id} collected Passenger {evt?.Passenger?.Id} from Floor {evt?.Floor?.Id}");

            // Simulate item processing
            //await Task.Delay(500);
            /*
            var elevator = _elevators.FirstOrDefault(e => e.Id == request.ElevatorId);
            if (elevator != null)
            {
                elevator.RequestedFloors.Add(request.DestinationFloor);
                elevator.CurrentDirection = request.DestinationFloor > elevator.CurrentFloor
                    ? Direction.Up
                    : Direction.Down;

                MoveElevatorToFloor(elevator, request.DestinationFloor);

                _eventBus.Publish(new ElevatorArrivedEvent
                {
                    FloorNumber = request.DestinationFloor,
                    ElevatorId = elevator.Id
                });
            }
            */
        }
    }
}
