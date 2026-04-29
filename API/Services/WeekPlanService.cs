using API.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Services
{
    public class WeekPlanService
    {
        private readonly AppDbContext m_appDbContext;
        private readonly IHttpContextAccessor m_httpContextAccessor;

        public WeekPlanService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            m_appDbContext = appDbContext;
            m_httpContextAccessor = httpContextAccessor;
        }

        //--------------------------------------------------
        // Week Plan
        //--------------------------------------------------
        public async Task<WeekPlan?> GetFullWeekPlanAsync(Guid userId)
        {
            return await m_appDbContext.WeekPlans
                .AsNoTracking()
                .Where(w => w.UserId == userId)
                .Include(w => w.Days)
                .ThenInclude(d => d.exercises)
                .FirstOrDefaultAsync();
        }
        //--------------------------------------------------
        // Get Current UserId
        //--------------------------------------------------
        private Guid? GetUserId()
        {
            var id = m_httpContextAccessor.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(id, out var guid) ? guid : null;
        }

        //--------------------------------------------------
        // Get All WeekPlans (FOR USER ONLY)
        //--------------------------------------------------
        public async Task<List<WeekPlan>> GetAllWeekPlansAsync()
        {
            var userId = GetUserId();

            if (userId == null)
                return new List<WeekPlan>();

            return await m_appDbContext.WeekPlans
                        .AsNoTracking()
                        .Where(w => w.UserId == userId)
                        .Include(w => w.Days)
                        .ThenInclude(d => d.exercises)
                        .ToListAsync();
        }

        //--------------------------------------------------
        // Get WeekPlan By Id (FOR USER ONLY)
        //--------------------------------------------------
        public async Task<WeekPlan?> GetWeekPlanByIdAsync(int id)
        {
            var userId = GetUserId();

            if (userId == null)
                return null;

            return await m_appDbContext.WeekPlans
                        .AsNoTracking()
                        .Include(w => w.Days)
                        .ThenInclude(d => d.exercises)
                        .Include(w => w.Meals)
                        .Include(w => w.Supplements)
                        .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
        }

        //--------------------------------------------------
        // Add WeekPlan (AUTO ASSIGN USER)
        //--------------------------------------------------
        public async Task<WeekPlan> AddWeekPlanAsync(WeekPlan weekPlan)
        {
            var userId = GetUserId();

            if (userId == null)
                throw new Exception("User not authenticated");

            bool exists = await m_appDbContext.WeekPlans
                         .AnyAsync(w => w.Label == weekPlan.Label && w.UserId == userId);

            if (exists)
                throw new Exception($"WeekPlan [{weekPlan.Label}] already exists");

            weekPlan.UserId = userId.Value;

            await m_appDbContext.AddAsync(weekPlan);
            await m_appDbContext.SaveChangesAsync();

            return weekPlan;
        }

        //--------------------------------------------------
        // Update WeekPlan (FOR USER ONLY)
        //--------------------------------------------------
        public async Task<WeekPlan> UpdateWeekPlanAsync(WeekPlan weekPlan)
        {
            var userId = GetUserId();

            var existPlan = await m_appDbContext.WeekPlans
                .FirstOrDefaultAsync(w =>
                    w.Id == weekPlan.Id &&
                    w.UserId == userId);

            if (existPlan == null)
                throw new Exception("WeekPlan not found");

            existPlan.Label = weekPlan.Label;
            existPlan.Note = weekPlan.Note;

            await m_appDbContext.SaveChangesAsync();
            return existPlan;
        }

        //--------------------------------------------------
        // Delete WeekPlan (FOR USER ONLY)
        //--------------------------------------------------
        public async Task<bool> DeleteWeekPlanAsync(int id)
        {
            var userId = GetUserId();

            var existPlan = await m_appDbContext.WeekPlans
                .FirstOrDefaultAsync(w =>
                    w.Id == id &&
                    w.UserId == userId);

            if (existPlan == null)
                return false;

            m_appDbContext.Remove(existPlan);
            await m_appDbContext.SaveChangesAsync();

            return true;
        }
    }
}