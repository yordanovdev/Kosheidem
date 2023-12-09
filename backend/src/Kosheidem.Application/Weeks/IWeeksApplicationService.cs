using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace Kosheidem.Weeks;

public interface IWeeksApplicationService : IApplicationService
{
    Task<ICollection<WeekOverviewDto>> GetAll();
}