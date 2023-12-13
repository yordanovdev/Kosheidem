using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Kosheidem.Meals.Dto;

namespace Kosheidem.Meals;

public interface IMealsApplicationService : IAsyncCrudAppService<MealDto, Guid>
{
    Task<List<MealTypeOverview>> GetMealsByType(MealsByTypeInputDto input);
    Task UpVoteMeal(UpVoteInputDto input);
    Task DownVoteMeal(DownVoteInputDto input);
}