using AtHash.Apps.ElevatorsDvt.Base.EventHandling.Events;
using AtHash.Apps.ElevatorsDvt.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Base.Controllers.Interfaces
{
    public interface IElevatorController
    {
        void HandleElevatorRequest(ElevatorRequestedEvent request);
        void HandleInsideButtonPress(InsideButtonPressedEvent request);
        List<ElevatorModel> GetElevators();
    }
}
