using API.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class WorkoutService
    {
        private readonly AppDbContext m_context;

        public WorkoutService(AppDbContext context)
        {
            m_context = context;
        }


        //--------------------------------------------------
        // Get All by WeekPlan
        //--------------------------------------------------
        public async Task<List<WorkoutDay>> GetAllByWeekPlanIdAsync(int weekPlanId)
        {
            return await m_context.WorkoutDays
                .Where(x => x.WeekPlanId == weekPlanId)
                .Include(x => x.exercises)
                .AsNoTracking()
                .ToListAsync();
        }

        //--------------------------------------------------
        // Get By Id
        //--------------------------------------------------
        public async Task<WorkoutDay?> GetByIdAsync(int id)
        {
            return await m_context.WorkoutDays
                .Include(x => x.exercises)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        //--------------------------------------------------
        // Add
        //--------------------------------------------------
        public async Task<WorkoutDay> AddAsync(int weekPlanId, WorkoutDay day)
        {
            day.WeekPlanId = weekPlanId;

            m_context.WorkoutDays.Add(day);
            await m_context.SaveChangesAsync();

            return day;
        }
         
        //--------------------------------------------------
        // Update
        //--------------------------------------------------
        public async Task<WorkoutDay> UpdateAsync(WorkoutDay day)
        {
            var exist = await m_context.WorkoutDays
                .Include(x => x.exercises)
                .FirstOrDefaultAsync(x => x.Id == day.Id);

            if (exist == null)
                throw new Exception("Not found");

            exist.DayName = day.DayName;
            exist.Focus = day.Focus;
            exist.IsRestDay = day.IsRestDay;
 
            exist.ExerciseType = day.ExerciseType;  
             

            await m_context.SaveChangesAsync();
            return exist;
        }

        //--------------------------------------------------
        // Delete
        //--------------------------------------------------
        public async Task<bool> DeleteAsync(int id)
        {
            var day = await m_context.WorkoutDays.FindAsync(id);
            if (day == null) return false;

            m_context.Remove(day);
            await m_context.SaveChangesAsync();

            return true;
        }
    }
}