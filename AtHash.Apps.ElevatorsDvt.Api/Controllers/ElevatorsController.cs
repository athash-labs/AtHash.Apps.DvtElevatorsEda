using AtHash.Apps.ElevatorsDvt.Api.Data;
using AtHash.Apps.ElevatorsDvt.Api.Dtos;
using AtHash.Apps.ElevatorsDvt.Api.Models;
using AtHash.Apps.ElevatorsDvt.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using AtHash.Apps.ElevatorsDvt.Api.Enumerations;
using AtHash.Apps.ElevatorsDvt.Api.Requests;

namespace AtHash.Apps.ElevatorsDvt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly BuildingDbContext _context;
        private readonly IElevatorService _elevatorService;

        public ElevatorsController(BuildingDbContext context, IElevatorService elevatorService)
        {
            _context = context;
            _elevatorService = elevatorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElevatorDto>>> GetElevators()
        {
            return await _context.Elevators
                .Include(e => e.Passengers)
                .Select(e => MapToElevatorDto(e))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ElevatorDto>> GetElevator(int id)
        {
            var elevator = await _context.Elevators
                .Include(e => e.Passengers)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (elevator == null)
            {
                return NotFound();
            }

            return MapToElevatorDto(elevator);
        }

        [HttpPost]
        public async Task<ActionResult<ElevatorDto>> PostElevator(ElevatorDto elevatorDto)
        {
            var building = await _context.Buildings.FindAsync(elevatorDto.BuildingId);
            if (building == null)
            {
                return BadRequest("Building not found");
            }

            var elevator = new ElevatorModel
            {
                Name = elevatorDto.Name,
                CurrentFloorId = 1, // Default to ground floor
                Status = ElevatorStatusEnum.Idle,
                BuildingId = elevatorDto.BuildingId
            };

            _context.Elevators.Add(elevator);
            await _context.SaveChangesAsync();

            elevatorDto.Id = elevator.Id;

            return CreatedAtAction(nameof(GetElevator), new { id = elevator.Id }, elevatorDto);
        }

        [HttpPost("{id}/call")]
        public async Task<ActionResult<ElevatorDto>> CallElevator(int id, [FromBody] CallRequest request)
        {
            try
            {
                var elevator = await _elevatorService.UpdateElevatorStatus(id, ElevatorStatusEnum.MovingUp);

                return Ok(elevator);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("building/{buildingId}")]
        public async Task<ActionResult<IEnumerable<ElevatorDto>>> GetElevatorsByBuilding(int buildingId)
        {
            try
            {
                var elevators = await _elevatorService.GetElevatorsByBuilding(buildingId);

                return Ok(elevators);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
    }
}
