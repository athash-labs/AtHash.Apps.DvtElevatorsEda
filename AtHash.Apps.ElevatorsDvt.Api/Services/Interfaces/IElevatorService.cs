using AtHash.Apps.ElevatorsDvt.Api.Dtos;
using AtHash.Apps.ElevatorsDvt.Api.Enumerations;

namespace AtHash.Apps.ElevatorsDvt.Api.Services.Interfaces
{
    public interface IElevatorService
    {
        Task<ElevatorDto> CallElevator(int floorId, int destinationFloor);
        Task<ElevatorDto> UpdateElevatorStatus(int elevatorId, ElevatorStatusEnum status);
        Task<List<ElevatorDto>> GetElevatorsByBuilding(int buildingId);
    }
}
