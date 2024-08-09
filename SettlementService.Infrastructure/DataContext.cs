using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SettlementService.Domain.Entities;
using SettlementService.Infrastructure.Configuration;

namespace SettlementService.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SettlementService");

            base.OnConfiguring(optionsBuilder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Deleted
                            && x.OriginalValues.Properties
                                .Any(p => p.Name.Contains("IsDeleted"))))
            {
                entity.State = EntityState.Unchanged;
                entity.CurrentValues["IsDeleted"] = true;
            }

            return base.SaveChangesAsync();
        }


        public DbSet<Booking> Bookings { get; set; }
    }
}
