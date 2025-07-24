using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling;

public record class FloorButtonPressedEvent(
    FloorButtonModel FloorButton)
        : IEvent;
