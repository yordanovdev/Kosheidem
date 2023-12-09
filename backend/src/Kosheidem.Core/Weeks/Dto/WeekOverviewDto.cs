using System;
using Abp.Application.Services.Dto;

namespace Kosheidem.Weeks;

public class WeekOverviewDto : EntityDto<Guid>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Future { get; set; }
    public bool Past { get; set; }
    public bool Current { get; set; }
}