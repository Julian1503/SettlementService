using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SettlementService.Constants.Booking;
using SettlementService.Domain.Entities;

namespace SettlementService.Infrastructure.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(booking => booking.Id).IsRequired();

            builder.Property(booking => booking.ClientName)
                .HasMaxLength(BookingConstants.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(booking => booking.BookingTime)
                .IsRequired();


            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
