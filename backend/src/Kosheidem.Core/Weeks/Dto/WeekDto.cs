using System;
using Abp.Application.Services.Dto;

namespace Kosheidem.Weeks;

public class WeekDto : EntityDto<Guid>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}