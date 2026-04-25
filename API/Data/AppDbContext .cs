using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
        {
        }
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<WeekPlan> WeekPlans { get; set; }
    }
}
