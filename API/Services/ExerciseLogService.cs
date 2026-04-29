using API.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ExerciseLogService
    {
        private readonly AppDbContext m_appDbContext;

        public ExerciseLogService(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext;
        }


        //--------------------------------------------------
        // Get All Logs
        //--------------------------------------------------
        public async Task<List<ExerciseLog>?> GetExerciseLogsAsync()
        {
            return await m_appDbContext.ExerciseLogs.Include(x => x.Exercise).ToListAsync();
        }


        //--------------------------------------------------
        // Get log by id
        //--------------------------------------------------
        public async Task<ExerciseLog?> GetExerciseLogByIdAsync(int id)
        {
            return await m_appDbContext.ExerciseLogs.FirstOrDefaultAsync(x => x.Id == id);
        }

        //--------------------------------------------------
        //Add new log
        //--------------------------------------------------
        public async Task<ExerciseLog> AddExerciseAsync(ExerciseLog newLog)
        {
            var exercise = await m_appDbContext.Exercises.FindAsync(newLog.ExerciseId);

            if (exercise == null)
                throw new Exception("Invalid ExerciseId");
            await m_appDbContext.ExerciseLogs.AddAsync(newLog);
            await m_appDbContext.SaveChangesAsync();
            return newLog;
        }
        //--------------------------------------------------
        //Update log
        //--------------------------------------------------
        public async Task<ExerciseLog?> UpdateExerciseAsync(ExerciseLog updateLog)
        {
            var exist = await m_appDbContext.ExerciseLogs.FirstOrDefaultAsync(lId => lId.Id == updateLog.Id);
            if (exist == null)
                throw new Exception("This Exercise Log is not exisit");
            exist.Reps = updateLog.Reps;
            exist.Weight = updateLog.Weight;
            exist.Notes = updateLog.Notes;
            exist.Date = updateLog.Date;
            await m_appDbContext.SaveChangesAsync();
            return exist;
        }

        //--------------------------------------------------
        //Remove log
        //--------------------------------------------------
        public async Task<bool> RemoveExerciseAsync(int id)
        {
            var exist = await m_appDbContext.ExerciseLogs.FirstOrDefaultAsync(lId => lId.Id == id);
            if (exist == null)
                throw new Exception("This Exercise Log is not exisit");
            m_appDbContext.Remove(exist);
            await m_appDbContext.SaveChangesAsync();
            return true;
        }
        //--------------------------------------------------
        //Get last log by exercise
        //--------------------------------------------------
        public async Task<List<ExerciseLog>> GetLastLogsByExerciseIdAsync(int exerciseId)
        {
            return await m_appDbContext.ExerciseLogs
                .Where(x => x.ExerciseId == exerciseId)
                .Include(x => x.Exercise)
                .OrderByDescending(x => x.Date)
                .Take(2)
                .ToListAsync();
        }
    }
}
