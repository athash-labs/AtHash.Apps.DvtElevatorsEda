namespace AtHash.Apps.ElevatorsDvt.Base.Models;

public class BuildingModel : BaseModel
{
    public IEnumerable<FloorModel> Floors { get; set; } = [];
}
