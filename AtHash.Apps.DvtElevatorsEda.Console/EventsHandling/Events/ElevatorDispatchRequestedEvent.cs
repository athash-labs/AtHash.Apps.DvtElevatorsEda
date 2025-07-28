using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Events;

public record class ElevatorDispatchRequestedEvent(
    PassengerModel Passenger,
    FloorModel CurrentFloor,
    FloorModel DestinationFloor
) : IEvent;
