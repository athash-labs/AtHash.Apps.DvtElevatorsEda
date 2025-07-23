using AtHash.Apps.DvtElevationsEda.Console.Models;

namespace AtHash.Apps.DvtElevatorsEda.Models;

public class ElevatorButtonModel : ElevatorRequestButtonModel
{
    public FloorModel DestinationFloor { get; set; }
}
