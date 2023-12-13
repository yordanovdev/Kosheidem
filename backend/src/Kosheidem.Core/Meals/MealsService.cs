using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Kosheidem.Meals.Dto;
using Kosheidem.MealVotes;
using Kosheidem.Weeks;
using Microsoft.EntityFrameworkCore;

namespace Kosheidem.Meals;

[AbpAuthorize]
public class MealsService : DomainService, IMealsService
{
    private readonly IRepository<Meal, Guid> _mealsRepository;
    private readonly IRepository<MealVote, Guid> _mealsVoteRepository;
    private readonly IWeeksService _weeksService;

    public MealsService(IRepository<Meal, Guid> mealsRepository, IRepository<MealVote, Guid> mealsVoteRepository,
        IWeeksService weeksService)
    {
        _mealsRepository = mealsRepository;
        _mealsVoteRepository = mealsVoteRepository;
        _weeksService = weeksService;
    }

    public async Task<List<MealTypeOverview>> GetMealsByType(MealsByTypeInputDto input)
    {
        var types = Enum.GetNames(typeof(MealTypes));
        var result = new List<MealTypeOverview>();

        var allMealVotes = _mealsVoteRepository.GetAllIncluding().Where(i => i.WeekId == input.WeekId);
        var lastWeek = await _weeksService.GetLastWeekById(input.WeekId);
        var votesLastWeek = lastWeek != null
            ? _mealsVoteRepository.GetAllIncluding().Where(i => i.WeekId == lastWeek.Id)
            : null;

        foreach (var type in types)
        {
            var mealType = Enum.Parse<MealTypes>(type);
            var meals = await _mealsRepository.GetAllIncluding()
                .Where(i => i.Type == mealType)
                .ToListAsync();

            var mealsResult = ObjectMapper.Map<List<MealDto>>(meals);

            foreach (var mealDto in mealsResult)
            {
                mealDto.NumberOfVotes = await allMealVotes.CountAsync(i => i.MealId == mealDto.Id);

                if (lastWeek != null)
                    mealDto.VotedLastWeek = await votesLastWeek.AnyAsync(i => i.MealId == mealDto.Id);
            }

            result.Add(new MealTypeOverview
            {
                Type = type,
                Meals = mealsResult
            });
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