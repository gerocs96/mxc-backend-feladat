using EventManager.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Tests.Utilities
{
    public static class InMemoryDbContextFactory
    {
        /// <summary>
        /// Creates in-memory database for testing
        /// </summary>
        public static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
