namespace AtHash.Apps.DvtElevatorsEda.Models;

public class BuildingModel : BaseModel
{
    public IEnumerable<FloorModel> Floors { get; set; } = [];
}
