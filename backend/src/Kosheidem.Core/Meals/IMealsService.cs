using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Kosheidem.Meals.Dto;

namespace Kosheidem.Meals;

public interface IMealsService : IDomainService
{
    Task<List<MealTypeOverview>> GetMealsByType(MealsByTypeInputDto input);
    Task UpMoteMeal(UpVoteInputDto input);
    Task DownVoteMeal(DownVoteInputDto input);
}