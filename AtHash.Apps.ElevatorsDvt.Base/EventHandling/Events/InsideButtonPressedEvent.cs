using AtHash.Apps.ElevatorsDvt.Base.Models;

namespace AtHash.Apps.ElevatorsDvt.Base.EventHandling.Events
{
    public class InsideButtonPressedEvent
    {
        public string ElevatorId { get; set; }
        public ElevatorModel Elevator { get; set; }
        public int DestinationFloorId { get; set; }
        public FloorModel DestinationFloor { get; set; }
    }
}
