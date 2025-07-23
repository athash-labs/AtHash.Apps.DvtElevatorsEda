using AtHash.Apps.DvtElevationsEda.Console.Events.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevationsEda.Console.Events;

public record class ElevatorButtonPressedEvent(
    ElevatorButtonModel ElevatorButton,
    ElevatorModel Floor
) : IEvent;
