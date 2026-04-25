using API.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class MealService
    {
        private readonly AppDbContext m_appDbContext;

        public MealService(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext;
        }

        //--------------------------------------------------
        // Get All Meals
        //--------------------------------------------------
        public async Task<List<Meal>> GetAllMealsAsync()
        {
            return await m_appDbContext.Meals.ToListAsync();
        }

        //--------------------------------------------------
        // Get Meal By Id
        //--------------------------------------------------
        public async Task<Meal?> GetMealByIdAsync(int id)
        {
            return await m_appDbContext.Meals
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //--------------------------------------------------
        // Get Meal By Name
        //--------------------------------------------------
        public async Task<Meal?> GetMealByNamAsync(string name)
        {
            return await m_appDbContext.Meals
                .FirstOrDefaultAsync(m => m.Name == name);
        }
        //--------------------------------------------------
        // Add Meal
        //--------------------------------------------------
        public async Task<Meal> AddMealAsync(Meal newMeal)
        {
            var existMeal = await m_appDbContext.Meals
                .FirstOrDefaultAsync(m => m.Name == newMeal.Name);

            if (existMeal != null)
                throw new Exception($"Meal [{newMeal.Name}] already exists");

            await m_appDbContext.AddAsync(newMeal);
            await m_appDbContext.SaveChangesAsync();
            return newMeal;
        }

        //--------------------------------------------------
        // Update Meal
        //--------------------------------------------------
        public async Task<Meal> UpdateMealAsync(Meal updateMeal)
        {
            var existMeal = await m_appDbContext.Meals
                .FindAsync(updateMeal.Id);

            if (existMeal == null)
                throw new Exception($"Meal with Id [{updateMeal.Id}] not found");

            existMeal.Name = updateMeal.Name;
            existMeal.Time = updateMeal.Time;
            existMeal.Calories = updateMeal.Calories;
            existMeal.Protein = updateMeal.Protein;
            existMeal.Tip = updateMeal.Tip;

            await m_appDbContext.SaveChangesAsync();
            return existMeal;
        }

        //--------------------------------------------------
        // Delete Meal
        //--------------------------------------------------
        public async Task<bool> DeleteMealAsync(int id)
        {
            var existMeal = await m_appDbContext.Meals.FindAsync(id);

            if (existMeal == null)
                return false;

            m_appDbContext.Remove(existMeal);
            await m_appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
