using AutoMapper;
using SettlementService.Domain.Entities;
using SettlementService.Interfaces.Model;

namespace SettlementService.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<BookingModel, Booking>(MemberList.Source);
            CreateMap<Booking, BookingModel>(MemberList.Destination);
        }
    }
}
