using AtHash.Apps.ElevatorsDvt.Api.Enumerations;

namespace AtHash.Apps.ElevatorsDvt.Api.Models;

public class FloorButtonModel : ElevatorRequestButtonModel
{
    public FloorModel Floor { get; set; }
    public FloorButtonDirectionEnum FloorButtonDirection { get; set; }
}
