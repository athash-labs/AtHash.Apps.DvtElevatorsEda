using AtHash.Apps.DvtElevatorsEda.Console.Events.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.Events;

public record class ElevatorButtonPressedEvent(
    ElevatorButtonModel ElevatorButton,
    ElevatorModel Floor
) : IEvent;
