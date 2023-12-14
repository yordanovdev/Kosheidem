using AutoMapper;
using Kosheidem.Authorization.Users;
using Kosheidem.Meals.Dto;

namespace Kosheidem.AutoMapper.Users;

public class UsersToUsersDtoModelProfile : Profile
{
    public UsersToUsersDtoModelProfile()
    {
        CreateMap<User, MealUserDto>();
    } 
}