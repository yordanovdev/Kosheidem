using System.Collections.Generic;

namespace Kosheidem.Meals.Dto;

public class MealTypeOverview
{
    public string Type { get; set; }
    public List<MealDto> Meals { get; set; } = new();
}