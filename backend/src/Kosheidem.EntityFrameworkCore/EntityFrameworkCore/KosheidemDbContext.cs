using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Kosheidem.Authorization.Roles;
using Kosheidem.Authorization.Users;
using Kosheidem.MultiTenancy;

namespace Kosheidem.EntityFrameworkCore
{
    public class KosheidemDbContext : AbpZeroDbContext<Tenant, Role, User, KosheidemDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public KosheidemDbContext(DbContextOptions<KosheidemDbContext> options)
            : base(options)
        {
        }
    }
}
