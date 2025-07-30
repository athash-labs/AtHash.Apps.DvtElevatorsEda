using AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Interfaces;
using AtHash.Apps.ElevatorsDvt.Base.Models;

namespace AtHash.Apps.ElevatorsDvt.Base.EventsHandling.Events;

public record class ElevatorButtonPressedEvent(
    ElevatorButtonModel ElevatorButton
) : IEvent
{
    public DateTime DateTimeCreated { get; } = DateTime.Now;
}