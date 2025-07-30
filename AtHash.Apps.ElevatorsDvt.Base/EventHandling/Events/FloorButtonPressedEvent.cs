using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Models;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;

public record class FloorButtonPressedEvent(
    FloorButtonModel FloorButton
) : IEvent
{
    public DateTime DateTimeCreated { get; } = DateTime.Now;
}