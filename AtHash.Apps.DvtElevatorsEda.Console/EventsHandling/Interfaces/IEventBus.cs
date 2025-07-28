using System.Threading.Tasks;

namespace AtHash.Apps.DvtElevatorsEda.Console.EventsHandling.Interfaces;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent evt)
        where TEvent : IEvent;
    Task SubscribeAsync<TEvent>(IEventHandler<TEvent> handler)
        where TEvent : IEvent;
}
