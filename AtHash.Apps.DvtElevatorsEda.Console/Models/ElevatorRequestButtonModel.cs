using AtHash.Apps.DvtElevatorsEda.Models;

namespace AtHash.Apps.DvtElevatorsEda.Console.Models;

public abstract class ElevatorRequestButtonModel : BaseModel
{
    public bool IsPressed { get; set; }
    public FloorModel Floor { get; set; }
}
