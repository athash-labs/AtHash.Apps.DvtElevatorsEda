using AtHash.Apps.ElevatorsDvt.Api.Data;
using AtHash.Apps.ElevatorsDvt.Api.Dtos;
using AtHash.Apps.ElevatorsDvt.Api.Models;
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

        public FloorsController(BuildingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FloorDto>>> GetFloors()
        {
            return await _context.Floors
                .Include(f => f.WaitingPassengers)
                .Select(f => MapToFloorDto(f))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FloorDto>> GetFloor(int id)
        {
            var floor = await _context.Floors
                .Include(f => f.WaitingPassengers)
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
                WaitingPassengerIds = floor.WaitingPassengers.Select(p => p.Id).ToList()
            };
        }
    }
}
