using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using Kosheidem.MealVotes;

namespace Kosheidem.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public string Picture { get; set; }
        public List<MealVote> MealVotes { get; set; }

        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>(),
                Picture = null,
                MealVotes = []
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}