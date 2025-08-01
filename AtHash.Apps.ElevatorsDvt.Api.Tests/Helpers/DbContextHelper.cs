using AtHash.Apps.ElevatorsDvt.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace AtHash.Apps.ElevatorsDvt.Api.Tests.Helpers
{
    public class DbContextHelper
    {
        public static BuildingDbContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<BuildingDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new BuildingDbContext(options);
        }
    }
}
