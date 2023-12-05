using Abp.Authorization;
using Kosheidem.Authorization.Roles;
using Kosheidem.Authorization.Users;

namespace Kosheidem.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
