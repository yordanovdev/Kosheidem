using System;

namespace Kosheidem.Meals.Dto;

public class DownVoteInputDto
{
    public Guid MealId { get; set; }
    public Guid WeekId { get; set; }
}