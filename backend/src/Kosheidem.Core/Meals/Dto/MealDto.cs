using System;
using Abp.Application.Services.Dto;

namespace Kosheidem.Meals.Dto;

public class MealDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int NumberOfVotes { get; set; }
    public bool VotedLastWeek { get; set; }
}