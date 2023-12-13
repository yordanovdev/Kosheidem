using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Kosheidem.Meals.Dto;

namespace Kosheidem.Meals;

public class MealsApplicationService : AsyncCrudAppService<Meal, MealDto, Guid>, IMealsApplicationService
{
    private readonly IMealsService _mealService;

    public MealsApplicationService(IRepository<Meal, Guid> repository, IMealsService mealService) : base(repository)
    {
        _mealService = mealService;
    }

    public async Task<List<MealTypeOverview>> GetMealsByType(MealsByTypeInputDto input)
    {
        var result = await _mealService.GetMealsByType(input);
        return result;
    }

    public async Task UpVoteMeal(UpVoteInputDto input)
    {
        await _mealService.UpMoteMeal(input);
    }
    
    public async Task DownVoteMeal(DownVoteInputDto input)
    {
        await _mealService.DownVoteMeal(input);
    }
}