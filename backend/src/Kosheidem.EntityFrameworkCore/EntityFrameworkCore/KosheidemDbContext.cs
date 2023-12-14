using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Kosheidem.Authorization.Roles;
using Kosheidem.Authorization.Users;
using Kosheidem.Meals;
using Kosheidem.MealVotes;
using Kosheidem.MultiTenancy;
using Kosheidem.Weeks;

namespace Kosheidem.EntityFrameworkCore
{
    public class KosheidemDbContext : AbpZeroDbContext<Tenant, Role, User, KosheidemDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<MealVote> MealVotes { get; set; }

        public KosheidemDbContext(DbContextOptions<KosheidemDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>()
                .Property(i => i.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Meal>()
                .HasMany(i => i.MealVotes)
                .WithOne(i => i.Meal)
                .HasForeignKey(i => i.MealId);

            modelBuilder.Entity<Week>()
                .HasMany(i => i.MealVotes)
                .WithOne(i => i.Week)
                .HasForeignKey(i => i.WeekId);

            modelBuilder.Entity<MealVote>()
                .HasMany(i => i.Users)
                .WithMany(i => i.MealVotes);

            modelBuilder.Entity<User>().Property(i => i.Picture).HasMaxLength(1000);

            base.OnModelCreating(modelBuilder);
        }
    }
}