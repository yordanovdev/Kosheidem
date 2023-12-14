using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Kosheidem.Authorization.Users;
using Kosheidem.Meals;
using Kosheidem.Weeks;

namespace Kosheidem.MealVotes;

public class MealVote : Entity<Guid>
{
    public Guid MealId { get; set; }
    public Guid WeekId { get; set; }
    public int NumberOfVotes { get; set; }
    public virtual Meal Meal { get; set; }
    public virtual Week Week { get; set; }
    public List<User> Users { get; set; }
}