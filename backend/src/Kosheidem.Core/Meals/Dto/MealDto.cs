using System;
using System.Collections;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Kosheidem.Authorization.Users;
using Microsoft.VisualBasic;

namespace Kosheidem.Meals.Dto;

public class MealDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int NumberOfVotes { get; set; }
    public bool VotedLastWeek { get; set; }
    public ICollection<MealUserDto> Users { get; set; } = new List<MealUserDto>();
}