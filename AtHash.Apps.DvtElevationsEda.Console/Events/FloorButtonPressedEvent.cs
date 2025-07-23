using AtHash.Apps.DvtElevationsEda.Console.Events.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevationsEda.Console.Events;

public record class FloorButtonPressedEvent(
    FloorButtonModel FloorButton,
    FloorModel Floor
) : IEvent;
