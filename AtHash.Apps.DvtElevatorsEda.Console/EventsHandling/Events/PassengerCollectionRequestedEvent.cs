using AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;
using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling;

public record class PassengerCollectionRequestedEvent(
    PassengerModel Passenger,
    FloorModel Floor
) : IEvent;
