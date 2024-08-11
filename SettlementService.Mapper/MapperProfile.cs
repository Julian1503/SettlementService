using AutoMapper;
using SettlementService.Domain.Entities;
using SettlementService.DTO.Booking;

namespace SettlementService.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<BookingDto, Booking>().ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClientName))
                .ForMember(dest => dest.BookingTime, opt => opt.MapFrom(src => src.BookingTime.ToString("HH:mm")));

            CreateMap<Booking, BookingDto>().ReverseMap()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BookingTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.BookingTime)));
        }
    }
}
