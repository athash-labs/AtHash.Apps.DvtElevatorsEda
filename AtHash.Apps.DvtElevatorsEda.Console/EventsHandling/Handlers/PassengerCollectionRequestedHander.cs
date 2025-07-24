using System;
using System.Threading.Tasks;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;

public class PassengerCollectionRequestedHander
{
    public async Task HandleAsync(PassengerCollectionRequestedEvent evt)
    {
        System.Console.WriteLine($"Handling: Passenger [{evt?.Passenger?.Id}] collection request on Floor: [{evt?.Floor?.Id}]");

        //ATL: Handle the passenger collection requested event     

        await Task.CompletedTask;
    }
}
