using AtHash.Apps.DvtElevatorsEda.Console.Events.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.Events;

public record class FloorButtonPressedEvent(
    FloorButtonModel FloorButton,
    FloorModel Floor
) : IEvent;
