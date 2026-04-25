using API.Data;
using API.Migrations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class WeekPlanService
    {
        private readonly AppDbContext m_appDbContext;

        public WeekPlanService(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext;
        }

        //--------------------------------------------------
        // Get All WeekPlans
        //--------------------------------------------------
        public async Task<List<WeekPlan>> GetAllWeekPlansAsync()
        {
            return await m_appDbContext.WeekPlans
                .Include(w => w.Days)
                    .ThenInclude(d => d.exercises)
                .ToListAsync();
        }

        //--------------------------------------------------
        // Get WeekPlan By Id
        //--------------------------------------------------
        public async Task<WeekPlan?> GetWeekPlanByIdAsync(int id)
        {
            return await m_appDbContext.WeekPlans
                .Include(w => w.Days)
                    .ThenInclude(d => d.exercises)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        //--------------------------------------------------
        // Add WeekPlan
        //--------------------------------------------------
        public async Task<WeekPlan> AddWeekPlanAsync(WeekPlan weekPlan)
        {
            var existPlan = await m_appDbContext.WeekPlans
                .FirstOrDefaultAsync(w => w.Label == weekPlan.Label);

            if (existPlan != null)
                throw new Exception($"WeekPlan [{weekPlan.Label}] already exists");

            await m_appDbContext.AddAsync(weekPlan);
            await m_appDbContext.SaveChangesAsync();
            return weekPlan;
        }

        //--------------------------------------------------
        // Update WeekPlan
        //--------------------------------------------------
        public async Task<WeekPlan> UpdateWeekPlanAsync(WeekPlan weekPlan)
        {
            var existPlan = await m_appDbContext.WeekPlans
                .FirstOrDefaultAsync(w => w.Id == weekPlan.Id);

            if (existPlan == null)
                throw new Exception($"WeekPlan with Id [{weekPlan.Id}] not found");

            existPlan.Label = weekPlan.Label;
            existPlan.Note = weekPlan.Note;

            await m_appDbContext.SaveChangesAsync();
            return existPlan;
        }

        //--------------------------------------------------
        // Delete WeekPlan
        //--------------------------------------------------
        public async Task<bool> DeleteWeekPlanAsync(int id)
        {
            var existPlan = await m_appDbContext.WeekPlans.FindAsync(id);

            if (existPlan == null)
                return false;

            m_appDbContext.Remove(existPlan);
            await m_appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
