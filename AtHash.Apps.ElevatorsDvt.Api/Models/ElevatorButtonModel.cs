using AtHash.Apps.ElevatorsDvt.Api.Models;

namespace AtHash.Apps.ElevatorsDvt.Api.Models;

public class ElevatorButtonModel : ElevatorRequestButtonModel
{
    public FloorModel DestinationFloor { get; set; }
}
