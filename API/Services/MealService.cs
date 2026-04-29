using API.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class MealService
    {
        private readonly AppDbContext m_context;

        public MealService(AppDbContext context)
        {
            m_context = context;
        }

        public async Task<List<Meal>> GetAllByWeekPlanIdAsync(int weekPlanId)
        {
                 return await m_context.Meals
                .Include(m => m.NutritionPlan)
                .Where(m => m.NutritionPlan.WeekPlanId == weekPlanId)
                .AsNoTracking()
                .ToListAsync();
        }
        //--------------------------------------------------
        // Get Meals by WeekPlan
        //--------------------------------------------------
        public async Task<List<Meal>> GetByWeekPlanAsync(int weekPlanId)
        {
            return await m_context.Meals
                .Where(x => x.NutritionPlan != null &&
                            x.NutritionPlan.WeekPlanId == weekPlanId)
                .AsNoTracking()
                .ToListAsync();
        }

        //--------------------------------------------------
        // Get by Id
        //--------------------------------------------------
        public async Task<Meal?> GetByIdAsync(int id)
        {
            return await m_context.Meals.FindAsync(id);
        }

        //--------------------------------------------------
        // Add Meal
        //--------------------------------------------------
        public async Task<Meal> AddAsync(int weekPlanId, Meal meal)
        {
            var nutritionPlan = await m_context.NutritionPlans
                .FirstOrDefaultAsync(n => n.WeekPlanId == weekPlanId);

            if (nutritionPlan == null)
                throw new Exception("NutritionPlan not found for this WeekPlan");

            meal.NutritionPlanId = nutritionPlan.Id;

            await m_context.Meals.AddAsync(meal);
            await m_context.SaveChangesAsync();

            return meal;
        }
        //--------------------------------------------------
        // Update
        //--------------------------------------------------
        public async Task<Meal> UpdateAsync(Meal meal)
        {
            var exist = await m_context.Meals.FindAsync(meal.Id);
            if (exist == null) throw new Exception("Not found");

            exist.Name = meal.Name;
            exist.Time = meal.Time;
            exist.Calories = meal.Calories;
            exist.Protein = meal.Protein;
            exist.Tip = meal.Tip;

            await m_context.SaveChangesAsync();
            return exist;
        }

        //--------------------------------------------------
        // Delete
        //--------------------------------------------------
        public async Task<bool> DeleteAsync(int id)
        {
            var meal = await m_context.Meals.FindAsync(id);
            if (meal == null) return false;

            m_context.Remove(meal);
            await m_context.SaveChangesAsync();

            return true;
        }
    }
}