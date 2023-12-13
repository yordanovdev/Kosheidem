using System;
using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;
using Kosheidem.MealVotes;

namespace Kosheidem.Meals;

public class Meal : FullAuditedEntity<Guid>
{
    public string Name { get; set; }
    public MealTypes Type { get; set; }

    public List<MealVote> MealVotes { get; set; }
}