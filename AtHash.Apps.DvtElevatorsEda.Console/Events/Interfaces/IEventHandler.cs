using System;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevatorsEda.Console.Events.Interfaces;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;

public interface IEventHandler<TEvent>
    where TEvent : IEvent
{
    Task HandleAsync(TEvent evt);
}