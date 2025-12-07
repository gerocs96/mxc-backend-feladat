using EventManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventManager.Api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
