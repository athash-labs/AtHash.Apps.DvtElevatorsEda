using AtHash.Apps.ElevatorsDvt.Api.Models;

namespace AtHash.Apps.ElevatorsDvt.Api.Requests
{
    public class CallRequest
    {
        public int FloorId { get; set; }
        public FloorModel Floor { get; set; }
        public int DestinationFloorId { get; set; }
        public FloorModel DestinationFloor { get; set; }
    }
}
