using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevationsEda.Console.Models;

public abstract class ElevatorRequestButtonModel : BaseModel
{
    public bool IsPressed { get; set; }
    public FloorModel Floor { get; set; }
}
