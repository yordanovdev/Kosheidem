using System;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;

namespace Kosheidem.Weeks;

public class WeeksBackgroundWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
{
    private readonly IRepository<Week, Guid> _weeksRepository;

    public WeeksBackgroundWorker(AbpTimer timer, IRepository<Week, Guid> weeksRepository) : base(timer)
    {
        _weeksRepository = weeksRepository;
        Timer.RunOnStart = true;
        Timer.Period = 60 * 60 * 1000;
    }

    [UnitOfWork]
    protected override void DoWork()
    {
        using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
        {
            var today = DateTime.Now;

            var timeTillNextMonday = (((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7);
            var timeTillEndOfWeek = timeTillNextMonday + 6;

            var dateMonday = DateTime.Now.AddDays(timeTillNextMonday);
            var dateEndOfWeek = DateTime.Now.AddDays(timeTillEndOfWeek);

            var dateExists = _weeksRepository.FirstOrDefault(i =>
                i.StartDate.Date == dateMonday.Date && i.EndDate.Date == dateEndOfWeek.Date);

            if (dateExists != null) return;

            var week = new Week
            {
                StartDate = dateMonday,
                EndDate = dateEndOfWeek
            };

            _weeksRepository.Insert(week);
        }
    }
}