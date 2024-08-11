using Microsoft.EntityFrameworkCore;
using SettlementService.Domain.Entities;

namespace SettlementService.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configuring the in-memory database
        /// </summary>
        /// <param name="optionsBuilder"></param>

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SettlementService");

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
