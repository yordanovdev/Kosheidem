using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
namespace Kosheidem.Weeks;

public class WeeksService : DomainService, IWeeksService
{
    private readonly IRepository<Week, Guid> _weeksRepository;

    public WeeksService(IRepository<Week, Guid> weeksRepository)
    {
        _weeksRepository = weeksRepository;
    }

    public async Task<ICollection<WeekDto>> GetAllWeeks()
    {
        var weeks = await _weeksRepository.GetAllIncluding().ToListAsync();
        return ObjectMapper.Map<ICollection<WeekDto>>(weeks);
    }
}