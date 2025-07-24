using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;

public record class PassengerCollectedEvent(
    PassengerModel Passenger,
    FloorModel Floor,
    ElevatorModel Elevator
) : IEvent;
