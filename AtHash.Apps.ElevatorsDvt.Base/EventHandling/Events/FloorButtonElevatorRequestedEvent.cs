using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Models;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;

public record class FloorButtonElevatorRequestedEvent(
    PassengerModel Passenger,
    FloorModel CurrentFloor,
    FloorModel DestinationFloor
) : IEvent
{
    public DateTime DateTimeCreated { get; } = DateTime.Now;
}