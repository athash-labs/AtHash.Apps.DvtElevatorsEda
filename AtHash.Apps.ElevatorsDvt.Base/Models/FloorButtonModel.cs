using AtHash.Apps.ElevatorsDvt.Base.Enumerations;

namespace AtHash.Apps.ElevatorsDvt.Base.Models;

public class FloorButtonModel : ElevatorRequestButtonModel
{
    public FloorModel Floor { get; set; }
    public FloorButtonDirectionEnum FloorButtonDirection { get; set; }
}
