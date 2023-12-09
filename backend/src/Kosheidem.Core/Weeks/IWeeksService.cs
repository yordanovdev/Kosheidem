using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Kosheidem.Weeks;

public interface IWeeksService : IDomainService
{
    Task<ICollection<Week>> GetAllWeeks();
}