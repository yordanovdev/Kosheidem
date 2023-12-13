using System;
using AutoMapper;
using Kosheidem.Weeks;

namespace Kosheidem.AutoMapper.Weeks;

public class WeeksToWeeksDtoModelProfile : Profile
{
    public WeeksToWeeksDtoModelProfile()
    {
        CreateMap<Week, WeekDto>();

        CreateMap<Week, WeekOverviewDto>()
            .ForMember(dest => dest.Past,
                opt => opt.MapFrom(src => src.EndDate < DateTime.Now))
            .ForMember(dest => dest.Future,
                opt => opt.MapFrom(src => src.StartDate > DateTime.Now))
            .ForMember(dest => dest.Current,
                opt => opt.MapFrom(src => src.StartDate < DateTime.Now && src.EndDate > DateTime.Now));
    }
}