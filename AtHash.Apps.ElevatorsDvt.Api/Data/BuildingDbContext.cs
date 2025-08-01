using AtHash.Apps.ElevatorsDvt.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;

namespace AtHash.Apps.ElevatorsDvt.Api.Data
{
    public class BuildingDbContext : DbContext
    {
        public BuildingDbContext(DbContextOptions<BuildingDbContext> options) : base(options) { }

        public DbSet<BuildingModel> Buildings { get; set; }
        public DbSet<FloorModel> Floors { get; set; }
        public DbSet<ElevatorModel> Elevators { get; set; }
        public DbSet<PassengerModel> Passengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<FloorModel>()
                .HasOne(f => f.Building)
                .WithMany(b => b.Floors)
                .HasForeignKey(f => f.BuildingId);

            modelBuilder.Entity<ElevatorModel>()
                .HasOne(e => e.Building)
                .WithMany(b => b.Elevators)
                .HasForeignKey(e => e.BuildingId);

            modelBuilder.Entity<PassengerModel>()
                .HasOne(p => p.Elevator)
                .WithMany(e => e.Passengers)
                .HasForeignKey(p => p.ElevatorId);
        }
    }
}
