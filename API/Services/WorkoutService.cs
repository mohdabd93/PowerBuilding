using API.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class WorkoutService
    {
        private readonly AppDbContext m_appDbContext;

        public WorkoutService(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext;
        }

        //----------------------------------------------------------------
        // Get All Days
        //----------------------------------------------------------------
        public async Task<List<WorkoutDay>> GetAllDaysAsync()
        {
            return await m_appDbContext.WorkoutDays
                .Include(d => d.exercises)
                .ToListAsync();
        }

        //----------------------------------------------------------------
        // Get Day By Id
        //----------------------------------------------------------------
        public async Task<WorkoutDay?> GetDayByIdAsync(int id)
        {
            return await m_appDbContext.WorkoutDays
                .Include(d => d.exercises)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        //----------------------------------------------------------------
        // Add New Day
        //----------------------------------------------------------------
        public async Task<WorkoutDay> addNewDayAsync(WorkoutDay newWorkoutDay)
        {
            var existDay = await m_appDbContext.WorkoutDays
                .FirstOrDefaultAsync(d => d.DayNumber == newWorkoutDay.DayNumber);

            if (existDay != null)
                throw new Exception($"Day [{newWorkoutDay.DayName}] already exists");

            var hasDuplicate = newWorkoutDay.exercises
                .GroupBy(e => e.Name)
                .Any(g => g.Count() > 1);

            if (hasDuplicate)
                throw new Exception($"Duplicate exercise in Day [{newWorkoutDay.DayName}]");

            await m_appDbContext.WorkoutDays.AddAsync(newWorkoutDay);
            await m_appDbContext.SaveChangesAsync();
            return newWorkoutDay;
        }

        //----------------------------------------------------------------
        // Update Day
        //----------------------------------------------------------------
        public async Task<WorkoutDay> UpdateDayAsync(WorkoutDay updateWorkoutDay)
        {
            var existDay = await m_appDbContext.WorkoutDays
                .FirstOrDefaultAsync(d => d.Id == updateWorkoutDay.Id);

            if (existDay == null)
                throw new Exception($"Day with Id [{updateWorkoutDay.Id}] not found");

            existDay.DayName = updateWorkoutDay.DayName;
            existDay.Focus = updateWorkoutDay.Focus;
            existDay.IsRestDay = updateWorkoutDay.IsRestDay;

            await m_appDbContext.SaveChangesAsync();
            return existDay;
        }

        //----------------------------------------------------------------
        // Delete Day
        //----------------------------------------------------------------
        public async Task<bool> DeleteDayAsync(int id)
        {
            var day = await m_appDbContext.WorkoutDays.FindAsync(id);

            if (day == null)
                return false;

            m_appDbContext.Remove(day);
            await m_appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
