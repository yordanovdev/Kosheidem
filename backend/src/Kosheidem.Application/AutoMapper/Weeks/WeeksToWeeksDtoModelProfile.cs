using AutoMapper;
using Kosheidem.Weeks;

namespace Kosheidem.AutoMapper.Weeks;

public class WeeksToWeeksDtoModelProfile : Profile
{
    public WeeksToWeeksDtoModelProfile()
    {
        CreateMap<Week, WeekDto>();
    }
}