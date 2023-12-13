using System;

namespace Kosheidem.Meals.Dto;

public class UpVoteInputDto
{
    public Guid MealId { get; set; }
    public Guid WeekId { get; set; }
}