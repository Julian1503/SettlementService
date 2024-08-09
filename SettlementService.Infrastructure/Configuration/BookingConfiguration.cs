using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SettlementService.Domain.Entities;

namespace SettlementService.Infrastructure.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(booking => booking.Id).IsRequired();

            builder.Property(booking => booking.ClientName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(booking => booking.BookingTime)
                .IsRequired();


            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
