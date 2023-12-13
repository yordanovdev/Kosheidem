using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Microsoft.EntityFrameworkCore;

namespace Kosheidem.Weeks;

[AbpAuthorize]
public class WeeksService : DomainService, IWeeksService
{
    private readonly IRepository<Week, Guid> _weeksRepository;

    public WeeksService(IRepository<Week, Guid> weeksRepository)
    {
        _weeksRepository = weeksRepository;
    }

    public async Task<ICollection<Week>> GetAllWeeks()
    {
        var weeks = await _weeksRepository.GetAllIncluding().ToListAsync();
        return weeks;
    }

    public async Task<Week> GetLastWeekById(Guid weekId)
    {
        var week = await _weeksRepository.GetAllIncluding().FirstOrDefaultAsync(i => i.Id == weekId);

        if (week == null) throw new UserFriendlyException("Week with that id was not found");

        var lastWeek = await _weeksRepository.GetAllIncluding().Where(i => i.StartDate < week.StartDate)
            .OrderByDescending(i => i.StartDate).FirstOrDefaultAsync();

        return lastWeek;
    }
}