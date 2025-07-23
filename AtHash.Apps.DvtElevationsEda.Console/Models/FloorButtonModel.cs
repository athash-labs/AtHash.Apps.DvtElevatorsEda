using AtHash.Apps.DvtElevationsEda.Console.Emunerations;
using AtHash.Apps.DvtElevationsEda.Console.Models;

namespace AtHash.Apps.DvtElevatorsEda.Models;

public class FloorButtonModel : ElevatorRequestButtonModel
{
    public FloorButtonDirectionEnum FloorButtonDirection { get; set; }
}
