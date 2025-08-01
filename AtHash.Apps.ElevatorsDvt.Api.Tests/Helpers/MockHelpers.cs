using AtHash.Apps.ElevatorsDvt.Api.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtHash.Apps.ElevatorsDvt.Api.Tests.Helpers
{
    public static class MockHelpers
    {
        public static Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        public static BuildingDbContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<BuildingDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new BuildingDbContext(options);
        }
    }
}
