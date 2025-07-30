using AtHash.Apps.ElevatorsDvt.Base.Models;

namespace AtHash.Apps.ElevatorsDvt.Base.Models;

public abstract class ElevatorRequestButtonModel : BaseModel
{
    public bool IsPressed { get; set; }
}
