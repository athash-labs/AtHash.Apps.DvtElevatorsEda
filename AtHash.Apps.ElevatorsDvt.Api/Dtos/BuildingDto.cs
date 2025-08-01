namespace AtHash.Apps.ElevatorsDvt.Api.Dtos
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<int> FloorIds { get; set; } = new List<int>();
        public List<int> ElevatorIds { get; set; } = new List<int>();
    }
}
