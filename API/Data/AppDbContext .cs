using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Principal;

namespace API.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Meal>()
        //        .HasOne(m => m.NutritionPlan)
        //        .WithMany(w => w.Meals)
        //        .HasForeignKey(m => m.NutritionPlan.WeekPlanId)
        //        .OnDelete(DeleteBehavior.NoAction);
        //}

        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<NutritionPlan> NutritionPlans { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<WeekPlan> WeekPlans { get; set; }
        public DbSet<ExerciseLog> ExerciseLogs { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<ExerciseTemplate> ExerciseTemplates { get; set; }
    }
}