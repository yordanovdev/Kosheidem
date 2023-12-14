using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Session;
using Abp.UI;
using Kosheidem.Authorization.Users;
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
    private readonly IRepository<User, long> _usersRepository;
    private readonly IWeeksService _weeksService;
    private readonly IAbpSession _abpSession;


    public MealsService(IRepository<Meal, Guid> mealsRepository, IRepository<MealVote, Guid> mealsVoteRepository,
        IWeeksService weeksService, IRepository<User, long> usersRepository, IAbpSession abpSession)
    {
        _mealsRepository = mealsRepository;
        _mealsVoteRepository = mealsVoteRepository;
        _weeksService = weeksService;
        _usersRepository = usersRepository;
        _abpSession = abpSession;
    }

    public async Task<List<MealTypeOverview>> GetMealsByType(MealsByTypeInputDto input)
    {
        var types = Enum.GetNames(typeof(MealTypes));
        var result = new List<MealTypeOverview>();

        var allMealVotes = _mealsVoteRepository.GetAllIncluding().Where(i => i.WeekId == input.WeekId)
            .Include(i => i.Users);
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
                var mealVotes = await allMealVotes.FirstOrDefaultAsync(i => i.MealId == mealDto.Id);

                mealDto.NumberOfVotes = await allMealVotes.CountAsync(i => i.MealId == mealDto.Id);

                if (mealVotes != null)
                {
                    mealDto.Users = ObjectMapper.Map<List<MealUserDto>>(mealVotes.Users);
                }

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
        var currentUserId = _abpSession.UserId;
        var user = await _usersRepository.GetAllIncluding().FirstOrDefaultAsync(i => i.Id == currentUserId);

        var meal = await _mealsRepository.GetAllIncluding()
            .Include(i => i.MealVotes)
            .ThenInclude(e => e.Users)
            .FirstOrDefaultAsync(i => i.Id == input.MealId);

        if (meal == null) throw new UserFriendlyException("Meal with that Id was not found");

        var voteEntity = meal.MealVotes.FirstOrDefault(i => i.WeekId == input.WeekId);
        if (voteEntity != null)
        {
            voteEntity.NumberOfVotes += 1;
            voteEntity.Users.Add(user);
        }
        else
        {
            meal.MealVotes.Add(new MealVote
            {
                MealId = input.MealId,
                WeekId = input.WeekId,
                NumberOfVotes = 1,
                Users = [user]
            });
        }
    }

    public async Task DownVoteMeal(DownVoteInputDto input)
    {
        var currentUserId = _abpSession.UserId;
        var user = await _usersRepository.GetAllIncluding().FirstOrDefaultAsync(i => i.Id == currentUserId);

        var meal = await _mealsRepository.GetAllIncluding()
            .Include(i => i.MealVotes)
            .ThenInclude(e => e.Users)
            .FirstOrDefaultAsync(i => i.Id == input.MealId);

        if (meal == null) throw new UserFriendlyException("Meal with that Id was not found");

        var voteEntity = meal.MealVotes.FirstOrDefault(i => i.WeekId == input.WeekId);
        if (voteEntity != null)
        {
            voteEntity.Users.Remove(user);

            voteEntity.NumberOfVotes -= 1;
            if (voteEntity.NumberOfVotes <= 0)
            {
                meal.MealVotes.Remove(voteEntity);
            }
        }
    }
}