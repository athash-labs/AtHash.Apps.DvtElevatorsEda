using AtHash.Apps.ElevatorsDvt.Api.Data;
using AtHash.Apps.ElevatorsDvt.Api.Dtos;
using AtHash.Apps.ElevatorsDvt.Api.Models;
using AtHash.Apps.ElevatorsDvt.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtHash.Apps.ElevatorsDvt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : ControllerBase
    {
        private readonly BuildingDbContext _context;
        private readonly IElevatorService _elevatorService;

        public FloorsController(BuildingDbContext context, IElevatorService elevatorService)
        {
            _context = context;
            _elevatorService = elevatorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FloorDto>>> GetFloors()
        {
            return await _context.Floors
                .Include(f => f.WaitingPassengersDown)
                .Include(f => f.WaitingPassengersUp)
                .Select(f => MapToFloorDto(f))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FloorDto>> GetFloor(int id)
        {
            var floor = await _context.Floors
                .Include(f => f.WaitingPassengersDown)
                .Include(f => f.WaitingPassengersUp)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (floor == null)
            {
                return NotFound();
            }

            return MapToFloorDto(floor);
        }

        [HttpPost]
        public async Task<ActionResult<FloorDto>> PostFloor(FloorDto floorDto)
        {
            var building = await _context.Buildings.FindAsync(floorDto.BuildingId);
            if (building == null)
            {
                return BadRequest("Building not found");
            }

            var floor = new FloorModel
            {
                FloorNumber = floorDto.FloorNumber,
                BuildingId = floorDto.BuildingId
            };

            _context.Floors.Add(floor);
            await _context.SaveChangesAsync();

            floorDto.Id = floor.Id;
            return CreatedAtAction(nameof(GetFloor), new { id = floor.Id }, floorDto);
        }

        private FloorDto MapToFloorDto(FloorModel floor)
        {
            return new FloorDto
            {
                Id = floor.Id,
                FloorNumber = floor.FloorNumber,
                BuildingId = floor.BuildingId,
                WaitingPassengerDownIds = floor.WaitingPassengersDown.Select(p => p.Id).ToList(),
                WaitingPassengerUpIds = floor.WaitingPassengersUp.Select(p => p.Id).ToList()
            };
        }

        private ElevatorDto MapToElevatorDto(ElevatorModel elevator)
        {
            return new ElevatorDto
            {
                Id = elevator.Id,
                Name = elevator.Name,
                CurrentFloor = elevator.CurrentFloor,
                Status = elevator.Status.ToString(),
                BuildingId = elevator.BuildingId,
                PassengerIds = elevator.Passengers.Select(p => p.Id).ToList()
            };
        }

        [HttpPost("{id}/call-up")]
        public async Task<ActionResult> PressUpButton(int id)
        {
            try
            {
                var floor = await _context.Floors.FindAsync(id);
                if (floor == null)
                {
                    return NotFound();
                }

                floor.UpButtonActive = true;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/call-down")]
        public async Task<ActionResult> PressDownButton(int id)
        {
            try
            {
                var floor = await _context.Floors.FindAsync(id);
                if (floor == null)
                {
                    return NotFound();
                }

                floor.DownButtonActive = true;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Controllers/ElevatorsController.cs
        [HttpPost("{id}/press-floor/{floorNumber}")]
        public async Task<ActionResult<ElevatorDto>> PressFloorButton(int id, int floorNumber)
        {
            try
            {
                var result = await _elevatorService.PressFloorButton(id, floorNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/board-passenger/{passengerId}")]
        public async Task<ActionResult<ElevatorDto>> BoardPassenger(int id, int passengerId)
        {
            try
            {
                var elevator = await _context.Elevators
                    .Include(e => e.Passengers)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (elevator == null)
                {
                    return NotFound("Elevator not found");
                }

                if (elevator.Passengers.Count >= elevator.MaximumPassengers)
                {
                    return BadRequest("Elevator is at full capacity");
                }

                var passenger = await _context.Passengers.FindAsync(passengerId);
                if (passenger == null)
                {
                    return NotFound("Passenger not found");
                }

                // Remove from waiting list if they were waiting
                var floor = await _context.Floors
                    .Include(f => f.WaitingPassengersUp)
                    .Include(f => f.WaitingPassengersDown)
                    .FirstOrDefaultAsync(f => f.FloorNumber == passenger.CurrentFloorId);

                if (floor != null)
                {
                    floor.WaitingPassengersUp.Remove(passenger);
                    floor.WaitingPassengersDown.Remove(passenger);

                    // Deactivate buttons if no more waiting passengers
                    if (floor.WaitingPassengersUp.Count == 0)
                    {
                        floor.UpButtonActive = false;
                    }
                    if (floor.WaitingPassengersDown.Count == 0)
                    {
                        floor.DownButtonActive = false;
                    }
                }

                elevator.Passengers.Add(passenger);
                passenger.ElevatorId = elevator.Id;
                await _context.SaveChangesAsync();

                return Ok(MapToElevatorDto(elevator));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/disembark-passenger/{passengerId}")]
        public async Task<ActionResult<ElevatorDto>> DisembarkPassenger(int id, int passengerId)
        {
            try
            {
                var elevator = await _context.Elevators
                    .Include(e => e.Passengers)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (elevator == null)
                {
                    return NotFound("Elevator not found");
                }

                var passenger = elevator.Passengers.FirstOrDefault(p => p.Id == passengerId);
                if (passenger == null)
                {
                    return NotFound("Passenger not found in this elevator");
                }

                // Remove from elevator
                elevator.Passengers.Remove(passenger);
                passenger.ElevatorId = null;

                // Remove the floor from requested floors if no more passengers going there
                if (!elevator.Passengers.Any(p => p.DestinationFloor == passenger.DestinationFloor))
                {
                    elevator.RequestedFloors.Remove(passenger.DestinationFloor);
                }

                await _context.SaveChangesAsync();
                return Ok(MapToElevatorDto(elevator));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
