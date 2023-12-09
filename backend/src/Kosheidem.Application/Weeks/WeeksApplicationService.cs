using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace Kosheidem.Weeks;

public class WeeksApplicationService : ApplicationService, IWeeksApplicationService
{
    private readonly IWeeksService _weeksService;

    public WeeksApplicationService(IWeeksService weeksService)
    {
        _weeksService = weeksService;
    }

    public async Task<ICollection<WeekOverviewDto>> GetAll()
    {
        var weeks = await _weeksService.GetAllWeeks();
        return ObjectMapper.Map<ICollection<WeekOverviewDto>>(weeks);
    }
}