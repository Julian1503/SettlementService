using Microsoft.Extensions.DependencyInjection;
using SettlementService.Domain.Abstractions;
using SettlementService.DTO.Booking;
using SettlementService.Infrastructure;
using SettlementService.Infrastructure.Repositories;
using SettlementService.Interfaces.Booking;
using SettlementService.Services.Booking;
using SettlementService.Validators;

namespace DependencyInjection
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Use this method to add all the dependencies to the service collection in the API
        /// </summary>
        /// <param name="services">A collection of services for the application to compose. This is useful for adding user provided or framework provided services.</param>
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IValidator<BookingDto>, BookingValidator>();
        }

    }
}
