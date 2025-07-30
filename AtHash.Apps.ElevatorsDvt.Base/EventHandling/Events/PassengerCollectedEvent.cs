using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Models;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;

public record class PassengerCollectedEvent(
    PassengerModel Passenger,
    FloorModel Floor,
    ElevatorModel Elevator = null
) : IEvent
{
    public DateTime DateTimeCreated { get; } = DateTime.Now;
}