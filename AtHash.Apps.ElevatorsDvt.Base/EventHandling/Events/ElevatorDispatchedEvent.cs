using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Models;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;

public record class ElevatorDispatchedEvent(
    ElevatorModel Elevator,
    FloorModel Floor
) : IEvent
{
    public DateTime DateTimeCreated { get; } = DateTime.Now;
}