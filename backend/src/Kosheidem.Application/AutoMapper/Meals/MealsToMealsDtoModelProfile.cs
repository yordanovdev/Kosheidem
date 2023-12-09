using AutoMapper;
using Kosheidem.Meals;
using Kosheidem.Meals.Dto;

namespace Kosheidem.AutoMapper.Meals;

public class MealsToMealsDtoModelProfile : Profile
{
    public MealsToMealsDtoModelProfile()
    {
        CreateMap<Meal, MealDto>().ForMember(dest => dest.Type,
            opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<MealDto, Meal>();
    }
}