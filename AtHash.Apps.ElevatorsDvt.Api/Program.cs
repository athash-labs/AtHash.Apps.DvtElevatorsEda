using AtHash.Apps.ElevatorsDvt.Api.Data;
using AtHash.Apps.ElevatorsDvt.Api.Enumerations;
using AtHash.Apps.ElevatorsDvt.Api.Models;
using AtHash.Apps.ElevatorsDvt.Api.Services;
using AtHash.Apps.ElevatorsDvt.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;

int _floorCount = 10;
int _elevatorCount = 4;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BuildingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IElevatorService, ElevatorService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BuildingDbContext>();
    context.Database.EnsureCreated();

    // Seed initial data if needed
    if (!context.Buildings.Any())
    {
        var building = new BuildingModel { Name = "Main Building" };
        context.Buildings.Add(building);

        // Add floors
        for (int i = 1; i <= _floorCount; i++)
        {
            context.Floors.Add(new FloorModel { FloorNumber = i, Building = building });
        }

        // Add elevators
        for (int i = 1; i <= _elevatorCount; i++)
        {
            context.Elevators.Add(new ElevatorModel
            {
                Name = $"Elevator {i}",
                CurrentFloorId = 1,
                Status = ElevatorStatusEnum.Idle,
                Building = building
            });
        }

        await context.SaveChangesAsync();
    }
}

app.Run();
