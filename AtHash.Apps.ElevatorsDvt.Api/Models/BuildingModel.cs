using System.Drawing;

namespace AtHash.Apps.ElevatorsDvt.Api.Models;

public class BuildingModel : BaseModel
{
    public List<FloorModel> Floors { get; set; } = new List<FloorModel>();
    public List<ElevatorModel> Elevators { get; set; } = new List<ElevatorModel>();
}
