using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Base.EventHandling.Interfaces
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> handler);
        void Publish<TEvent>(TEvent evt);
    }
}
