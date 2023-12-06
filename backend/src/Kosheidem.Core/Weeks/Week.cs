using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Kosheidem.MealVotes;

namespace Kosheidem.Weeks;

public class Week : Entity<Guid>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<MealVote> MealVotes { get; set; }
}