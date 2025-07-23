using AtHash.Apps.DvtElevatorsEda.Console.Emunerations;
using AtHash.Apps.DvtElevatorsEda.Console.Models;

namespace AtHash.Apps.DvtElevatorsEda.Models;

public class FloorButtonModel : ElevatorRequestButtonModel
{
    public FloorButtonDirectionEnum FloorButtonDirection { get; set; }
}
