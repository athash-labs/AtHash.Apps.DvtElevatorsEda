using AtHash.Apps.ElevatorsDvt.Api.Data;
using AtHash.Apps.ElevatorsDvt.Api.Dtos;
using AtHash.Apps.ElevatorsDvt.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AtHash.Apps.ElevatorsDvt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly BuildingDbContext _context;

        public BuildingsController(BuildingDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpPost(Name = "GetBuildings")]
        public async Task<ActionResult<IEnumerable<BuildingDto>>> GetBuildings()
        {
            return await _context.Buildings
                .Include(b => b.Floors)
                .Include(b => b.Elevators)
                .Select(b => MapToBuildingDTO(b))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingDto>> GetBuilding(int id)
        {
            var building = await _context.Buildings
                .Include(b => b.Floors)
                .Include(b => b.Elevators)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (building == null)
            {
                return NotFound();
            }

            return MapToBuildingDTO(building);
        }

        [HttpPost(Name = "PostBuilding")]
        public async Task<ActionResult<BuildingDto>> PostBuilding(BuildingDto buildingDto)
        {
            var building = new BuildingModel
            {
                Name = buildingDto.Name
            };

            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();

            buildingDto.Id = building.Id;

            return CreatedAtAction(nameof(GetBuilding), new { id = building.Id }, buildingDto);
        }

        [HttpGet(Name = "MapToBuildingDTO")]
        private BuildingDto MapToBuildingDTO(BuildingModel building)
        {
            return new BuildingDto
            {
                Id = building.Id,
                Name = building.Name,
                FloorIds = building.Floors.Select(f => f.Id).ToList(),
                ElevatorIds = building.Elevators.Select(e => e.Id).ToList()
            };
        }

        [HttpGet(Name = "GetBuildings")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                //Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
