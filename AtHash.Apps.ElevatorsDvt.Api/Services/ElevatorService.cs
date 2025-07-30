using AtHash.Apps.ElevatorsDvt.Api.Data;
using AtHash.Apps.ElevatorsDvt.Api.Dtos;
using AtHash.Apps.ElevatorsDvt.Api.Enumerations;
using AtHash.Apps.ElevatorsDvt.Api.Models;
using AtHash.Apps.ElevatorsDvt.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AtHash.Apps.ElevatorsDvt.Api.Services
{
    public class ElevatorService : IElevatorService
    {
        private readonly BuildingDbContext _context;

        public ElevatorService(BuildingDbContext context)
        {
            _context = context;
        }

        public async Task<ElevatorDto> CallElevator(int floorId, int destinationFloor)
        {
            var floor = await _context.Floors.FindAsync(floorId);

            if (floor == null)
            {
                throw new ArgumentException("Floor not found");
            }

            // Find the nearest available elevator
            var availableElevators = await _context.Elevators
                .Where(e => e.BuildingId == floor.BuildingId
                    && (e.Status == ElevatorStatusEnum.Idle
                        || e.Status == ElevatorStatusEnum.MovingUp && e.CurrentFloor.Id <= floor.FloorNumber
                        || e.Status == ElevatorStatusEnum.MovingDown && e.CurrentFloor.Id >= floor.FloorNumber))
                .ToListAsync();

            if (!availableElevators.Any())
            {
                throw new Exception("No available elevators");
            }

            var nearestElevator = availableElevators
                .OrderBy(e => Math.Abs(e.CurrentFloorId - floor.FloorNumber))
                .First();

            nearestElevator.Status = floor.Id > nearestElevator.CurrentFloor.Id
                ? ElevatorStatusEnum.MovingUp
                : ElevatorStatusEnum.MovingDown;

            await _context.SaveChangesAsync();

            return MapToElevatorDTO(nearestElevator);
        }

        public async Task<List<ElevatorDto>> GetElevatorsByBuilding(int buildingId)
        {
            var elevators = await _context.Elevators
                .Where(e => e.BuildingId == buildingId)
                .ToListAsync();

            return elevators.Select(MapToElevatorDTO).ToList();
        }

        public async Task<ElevatorDto> UpdateElevatorStatus(int elevatorId, ElevatorStatusEnum status)
        {
            var elevator = await _context.Elevators.FindAsync(elevatorId);

            if (elevator == null)
            {
                throw new ArgumentException("Elevator not found");
            }

            elevator.Status = status;
            await _context.SaveChangesAsync();

            return MapToElevatorDTO(elevator);
        }

        private ElevatorDto MapToElevatorDTO(ElevatorModel elevator)
        {
            return new ElevatorDto
            {
                Id = elevator.Id,
                Name = elevator.Name,
                CurrentFloorId = elevator.CurrentFloorId,
                CurrentFloor = elevator.CurrentFloor,
                Status = elevator.Status.ToString(),
                BuildingId = elevator.BuildingId,
                PassengerIds = elevator.Passengers.Select(p => p.Id).ToList()
            };
        }
    }
}
