using System;
using System.Threading.Tasks;
using AtHash.Apps.DvtElevationsEda.Console.Events.Interfaces;

namespace AtHash.Apps.DvtElevationsEda.Console.Events.Handlers.Interfaces;

public interface IEventHandler<TEvent>
    where TEvent : IEvent
{
    Task HandleAsync(TEvent evt);
}