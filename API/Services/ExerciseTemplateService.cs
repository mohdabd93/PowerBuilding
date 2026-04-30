using API.Data;
using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ExerciseTemplateService
    {
        private readonly AppDbContext m_context;

        public ExerciseTemplateService(AppDbContext context)
        {
            m_context = context;
        }

        //--------------------------------------------------
        // Get All
        //--------------------------------------------------
        public async Task<List<ExerciseTemplate>> GetAllAsync()
        {
            return await m_context.ExerciseTemplates
                .Where(x => x.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        //--------------------------------------------------
        // GetFilteredAsync
        //--------------------------------------------------
        public async Task<List<ExerciseTemplate>> GetFilteredAsync(TargetMuscle muscle, ExerciseType type)
        {
            return await m_context.ExerciseTemplates
                .Where(x => x.Muscle == muscle
                         && x.DefaultType == type
                         && x.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        //--------------------------------------------------
        // Get By Muscle
        //--------------------------------------------------
        public async Task<List<ExerciseTemplate>> GetByMuscleAsync(TargetMuscle muscle)
        {
            return await m_context.ExerciseTemplates
                .Where(x => x.Muscle == muscle && x.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        //--------------------------------------------------
        // Add
        //--------------------------------------------------
        public async Task<ExerciseTemplate> AddAsync(ExerciseTemplate model)
        {
            // FIX: reset Id so SQL Server generates it via IDENTITY
            model.Id = 0;

            m_context.ExerciseTemplates.Add(model);
            await m_context.SaveChangesAsync();
            return model;
        }

        //--------------------------------------------------
        // Update
        //--------------------------------------------------
        public async Task<ExerciseTemplate> UpdateAsync(ExerciseTemplate model)
        {
            var exist = await m_context.ExerciseTemplates.FindAsync(model.Id);
            if (exist == null)
                throw new Exception("Template not found");

            exist.Name = model.Name;
            exist.Muscle = model.Muscle;
            exist.DefaultType = model.DefaultType;
            exist.Variation = model.Variation;
            exist.Description = model.Description;

            await m_context.SaveChangesAsync();
            return exist;
        }

        //--------------------------------------------------
        // Delete (Soft delete)
        //--------------------------------------------------
        public async Task<bool> DeleteAsync(int id)
        {
            var exist = await m_context.ExerciseTemplates.FindAsync(id);
            if (exist == null)
                return false;

            exist.IsActive = false;
            await m_context.SaveChangesAsync();
            return true;
        }
    }
}