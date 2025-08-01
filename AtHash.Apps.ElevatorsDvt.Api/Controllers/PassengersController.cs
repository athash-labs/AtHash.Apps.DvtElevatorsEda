using AtHash.Apps.ElevatorsDvt.Api.Data;
using AtHash.Apps.ElevatorsDvt.Api.Dtos;
using AtHash.Apps.ElevatorsDvt.Api.Models;
using AtHash.Apps.ElevatorsDvt.Api.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtHash.Apps.ElevatorsDvt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly BuildingDbContext _context;

        public PassengersController(BuildingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassengerDto>>> GetPassengers()
        {
            return await _context.Passengers
                .Select(p => MapToPassengerDto(p))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerDto>> GetPassenger(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return MapToPassengerDto(passenger);
        }

        [HttpPost]
        public async Task<ActionResult<PassengerDto>> PostPassenger(PassengerDto passengerDto)
        {
            var passenger = new PassengerModel
            {
                Name = passengerDto.Name,
                CurrentFloor = passengerDto.CurrentFloor,
                DestinationFloor = passengerDto.DestinationFloor,
                CallTime = DateTime.UtcNow,
                ElevatorId = passengerDto.ElevatorId
            };

            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();

            passengerDto.Id = passenger.Id;
            return CreatedAtAction(nameof(GetPassenger), new { id = passenger.Id }, passengerDto);
        }

        [HttpPost("call")]
        public async Task<ActionResult<PassengerDto>> CallElevator([FromBody] PassengerCallRequest request)
        {
            var floor = await _context.Floors
                .FirstOrDefaultAsync(f => f.FloorNumber == request.CurrentFloorId
                    && f.BuildingId == request.BuildingId);

            if (floor == null)
            {
                return BadRequest("Floor not found in the specified building");
            }

            var passenger = new PassengerModel
            {
                Name = request.Name ?? "Anonymous",
                CurrentFloor = request.CurrentFloor,
                DestinationFloor = request.DestinationFloor,
                CallTime = DateTime.UtcNow
            };

            floor.WaitingPassengers.Add(passenger);
            await _context.SaveChangesAsync();

            var passengerDTO = MapToPassengerDto(passenger);
            return CreatedAtAction(nameof(GetPassenger), new { id = passenger.Id }, passengerDTO);
        }
        [HttpPost("call-down")]
        public async Task<ActionResult<PassengerDto>> CallElevatorDown([FromBody] PassengerCallRequest request)
        {
            var floor = await _context.Floors
                .FirstOrDefaultAsync(f => f.FloorNumber == request.CurrentFloorId
                    && f.BuildingId == request.BuildingId);

            if (floor == null)
            {
                return BadRequest("Floor not found in the specified building");
            }

            var passenger = new PassengerModel
            {
                Name = request.Name ?? "Anonymous",
                CurrentFloor = request.CurrentFloor,
                DestinationFloor = request.DestinationFloor,
                CallTime = DateTime.UtcNow
            };

            floor.WaitingPassengersDown.Add(passenger);
            await _context.SaveChangesAsync();

            var passengerDto = MapToPassengerDto(passenger);
            return CreatedAtAction(nameof(GetPassenger), new { id = passenger.Id }, passengerDto);
        }

        [HttpPost("call-up")]
        public async Task<ActionResult<PassengerDto>> CallElevatorUp([FromBody] PassengerCallRequest request)
        {
            var floor = await _context.Floors
                .FirstOrDefaultAsync(f => f.FloorNumber == request.CurrentFloorId
                    && f.BuildingId == request.BuildingId);

            if (floor == null)
            {
                return BadRequest("Floor not found in the specified building");
            }

            var passenger = new PassengerModel
            {
                Name = request.Name ?? "Anonymous",
                CurrentFloor = request.CurrentFloor,
                DestinationFloor = request.DestinationFloor,
                CallTime = DateTime.UtcNow
            };

            floor.WaitingPassengersUp.Add(passenger);
            await _context.SaveChangesAsync();

            var passengerDto = MapToPassengerDto(passenger);
            return CreatedAtAction(nameof(GetPassenger), new { id = passenger.Id }, passengerDto);
        }

        private PassengerDto MapToPassengerDto(PassengerModel passenger)
        {
            return new PassengerDto
            {
                Id = passenger.Id,
                Name = passenger.Name,
                CurrentFloor = passenger.CurrentFloor,
                DestinationFloor = passenger.DestinationFloor,
                CallTime = passenger.CallTime,
                ElevatorId = passenger.ElevatorId
            };
        }
    }
}
