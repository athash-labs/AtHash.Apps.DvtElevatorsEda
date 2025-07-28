using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;

public record class ElevatorDispatchedEvent(
    ElevatorModel Elevator,
    FloorModel Floor
) : IEvent;
