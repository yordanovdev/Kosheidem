using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Kosheidem.Meals.Dto;
using Kosheidem.MealVotes;
using Microsoft.EntityFrameworkCore;

namespace Kosheidem.Meals;

public class MealsService : DomainService, IMealsService
{
    private readonly IRepository<Meal, Guid> _mealsRepository;
    private readonly IRepository<MealVote, Guid> _mealsVoteRepository;

    public MealsService(IRepository<Meal, Guid> mealsRepository, IRepository<MealVote, Guid> mealsVoteRepository)
    {
        _mealsRepository = mealsRepository;
        _mealsVoteRepository = mealsVoteRepository;
    }

    public async Task<List<MealTypeOverview>> GetMealsByType(MealsByTypeInputDto input)
    {
        var types = Enum.GetNames(typeof(MealTypes)).ToList();

        var result = new List<MealTypeOverview>();

        foreach (var type in types)
        {
            var meals = await _mealsRepository.GetAllIncluding()
                .Where(i => i.Type == Enum.Parse<MealTypes>(type))
                .ToListAsync();

            var mealsResult = ObjectMapper.Map<List<MealDto>>(meals);

            foreach (var mealDto in mealsResult)
            {
                var count = await _mealsVoteRepository.GetAllIncluding()
                    .CountAsync(i => i.WeekId == input.WeekId && i.MealId == mealDto.Id);
                mealDto.NumberOfVotes = count;
            }

            var item = new MealTypeOverview
            {
                Type = type,
                Meals = mealsResult
            };


            result.Add(item);
        }

        return result;
    }


    public async Task UpMoteMeal(UpVoteInputDto input)
    {
        var meal = await _mealsRepository.GetAllIncluding().Include(i => i.MealVotes)
            .FirstOrDefaultAsync(i => i.Id == input.MealId);

        if (meal == null) throw new UserFriendlyException("Meal with that Id was not found");

        var voteEntity = meal.MealVotes.FirstOrDefault(i => i.WeekId == input.WeekId);
        if (voteEntity != null)
        {
            voteEntity.NumberOfVotes += 1;
        }
        else
        {
            meal.MealVotes.Add(new MealVote
            {
                MealId = input.MealId,
                WeekId = input.WeekId,
                NumberOfVotes = 1
            });
        }
    }

    public async Task DownVoteMeal(DownVoteInputDto input)
    {
        var meal = await _mealsRepository.GetAllIncluding().Include(i => i.MealVotes)
            .FirstOrDefaultAsync(i => i.Id == input.MealId);

        if (meal == null) throw new UserFriendlyException("Meal with that Id was not found");

        var voteEntity = meal.MealVotes.FirstOrDefault(i => i.WeekId == input.WeekId);
        if (voteEntity != null)
        {
            voteEntity.NumberOfVotes -= 1;
            if (voteEntity.NumberOfVotes <= 0)
            {
                meal.MealVotes.Remove(voteEntity);
            }
        }
    }
}