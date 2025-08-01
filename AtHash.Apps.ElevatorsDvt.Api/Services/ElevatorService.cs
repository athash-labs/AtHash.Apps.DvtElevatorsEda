using AtHash.Apps.ElevatorsDvt.Api.Data;
using AtHash.Apps.ElevatorsDvt.Api.Dtos;
using AtHash.Apps.ElevatorsDvt.Api.Enumerations;
using AtHash.Apps.ElevatorsDvt.Api.Models;
using AtHash.Apps.ElevatorsDvt.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AtHash.Apps.ElevatorsDvt.Api.Services
{
    public class ElevatorService : IElevatorService
    {
        private readonly BuildingDbContext _context;

        public ElevatorService(BuildingDbContext context)
        {
            _context = context;
        }

        public async Task<ElevatorDto> CallElevator(int floorId, ElevatorDirectionEnum direction)
        {
            var floor = await _context.Floors.FindAsync(floorId);

            if (floor == null)
            {
                throw new ArgumentException("Floor not found");
            }

            // Activate the appropriate button
            if (direction == ElevatorDirectionEnum.GoingUp)
            {
                floor.UpButtonActive = true;
            }
            else
            {
                floor.DownButtonActive = true;
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
                .FirstOrDefault();

            nearestElevator.Status = floor.Id > nearestElevator.CurrentFloor.Id
                ? ElevatorStatusEnum.MovingUp
                : ElevatorStatusEnum.MovingDown;

            await _context.SaveChangesAsync();

            return MapToElevatorDto(nearestElevator);
        }

        public async Task<List<ElevatorDto>> GetElevatorsByBuilding(int buildingId)
        {
            var elevators = await _context.Elevators
                .Where(e => e.BuildingId == buildingId)
                .ToListAsync();

            return elevators.Select(MapToElevatorDto).ToList();
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

            return MapToElevatorDto(elevator);
        }

        private ElevatorDto MapToElevatorDto(ElevatorModel elevator)
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

        public async Task<ElevatorDto> PressFloorButton(int elevatorId, int floorNumber)
        {
            var elevator = await _context.Elevators
                .Include(e => e.Building)
                .ThenInclude(b => b.Floors)
                .FirstOrDefaultAsync(e => e.Id == elevatorId);

            if (elevator == null)
            {
                throw new ArgumentException("Elevator not found");
            }

            // Verify the floor exists in the building
            var buildingFloorNumbers = elevator.Building
                .Floors
                .Select(f => f.FloorNumber)
                .ToList();

            if (!buildingFloorNumbers.Contains(floorNumber))
            {
                throw new ArgumentException("Invalid floor number for this building");
            }

            // Add to requested floors if not already there
            if (!elevator.RequestedFloorIds.Contains(floorNumber))
            {
                elevator.RequestedFloorIds.Add(floorNumber);

                // Sort based on current direction
                if (elevator.Status == ElevatorStatusEnum.MovingUp)
                {
                    elevator.RequestedFloors.Sort();
                }
                else if (elevator.Status == ElevatorStatusEnum.MovingDown)
                {
                    elevator.RequestedFloorIds.Sort((a, b) => b.CompareTo(a));
                }
            }

            await _context.SaveChangesAsync();

            return MapToElevatorDto(elevator);
        }
    }
}
