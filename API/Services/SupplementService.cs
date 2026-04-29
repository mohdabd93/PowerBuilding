using API.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class SupplementService
    {
        private readonly AppDbContext m_context;

        public SupplementService(AppDbContext context)
        {
            m_context = context;
        }

        //--------------------------------------------------
        // GetAllByWeekPlanIdAsync
        //--------------------------------------------------
        public async Task<List<Supplement>> GetAllByWeekPlanIdAsync(int weekPlanId)
        {
            return await m_context.Supplements
                .Where(m => m.WeekPlanId == weekPlanId)
                .AsNoTracking()
                .ToListAsync();
        }

        //--------------------------------------------------
        // Get by WeekPlan
        //--------------------------------------------------
        public async Task<List<Supplement>> GetByWeekPlanAsync(int weekPlanId)
        {
            return await m_context.Supplements
                .Where(x => x.WeekPlanId == weekPlanId)
                .AsNoTracking()
                .ToListAsync();
        }

        //--------------------------------------------------
        // Get by Id
        //--------------------------------------------------
        public async Task<Supplement?> GetByIdAsync(int id)
        {
            return await m_context.Supplements.FindAsync(id);
        }

        //--------------------------------------------------
        // Add
        //--------------------------------------------------
        public async Task<Supplement> AddAsync(int weekPlanId, Supplement model)
        {
            model.WeekPlanId = weekPlanId;

            m_context.Supplements.Add(model);
            await m_context.SaveChangesAsync();

            return model;
        }

        //--------------------------------------------------
        // Update
        //--------------------------------------------------
        public async Task<Supplement> UpdateAsync(Supplement s)
        {
            var exist = await m_context.Supplements.FindAsync(s.Id);
            if (exist == null) throw new Exception("Not found");

            exist.Name = s.Name;
            exist.Dose = s.Dose;
            exist.Description = s.Description;
            exist.IsEssential = s.IsEssential;

            await m_context.SaveChangesAsync();
            return exist;
        }

        //--------------------------------------------------
        // Delete
        //--------------------------------------------------
        public async Task<bool> DeleteAsync(int id)
        {
            var s = await m_context.Supplements.FindAsync(id);
            if (s == null) return false;

            m_context.Remove(s);
            await m_context.SaveChangesAsync();

            return true;
        }
    }
}