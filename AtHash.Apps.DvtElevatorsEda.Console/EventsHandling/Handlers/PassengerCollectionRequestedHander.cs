using System;
using System.Threading.Tasks;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Handlers;

public class PassengerCollectionRequestedHander
{
    public async Task HandleAsync(PassengerCollectionRequestedEvent evt)
    {
        System.Console.WriteLine($"Handling: Floor button [{evt?.FloorButton?.Id}] on Floor: [{evt?.FloorButton?.Floor?.Id}] going [{evt?.FloorButton?.FloorButtonDirection}]");

        //ATL: Handle the passenger collection requested event     

        await Task.CompletedTask;
    }
}
