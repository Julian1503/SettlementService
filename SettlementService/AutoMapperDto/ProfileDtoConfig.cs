using AutoMapper;
using SettlementService.API.Dto;
using SettlementService.Interfaces.Model;

namespace SettlementService.API.AutoMapperDto
{
    public class ProfileDtoConfig : Profile
    {
        public ProfileDtoConfig()
        {
            CreateMap<BookingDto, BookingModel>()
                       .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Name))
                       .ForMember(dest => dest.BookingTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.BookingTime)));

            CreateMap<BookingModel, BookingDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClientName))
                .ForMember(dest => dest.BookingTime, opt => opt.MapFrom(src => src.BookingTime.ToString("HH:mm")));
        }
    }
}
