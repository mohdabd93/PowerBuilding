using API.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class SupplementService
    {
        private readonly AppDbContext m_appDbContext;

        public SupplementService(AppDbContext appDbContext)
        {
            m_appDbContext = appDbContext;
        }


        //--------------------------------------------------
        // Get All Supplements
        //--------------------------------------------------
        public async Task<IEnumerable<Supplement>> GetAllSupplementsAsync()
        {
            return await m_appDbContext.Supplements.ToListAsync();
        }

        //--------------------------------------------------
        // Get GetSupplement By Id  
        //--------------------------------------------------
        public async Task<Supplement?> GetSupplementByIdAsync(int id)
        {
            return await m_appDbContext.Supplements
        .FirstOrDefaultAsync(m => m.Id == id);
        }

        //--------------------------------------------------
        // Add Supplement Async
        //--------------------------------------------------
        public async Task<Supplement> AddSupplementAsync(Supplement supplement)
        {
            var existSUpplement = await m_appDbContext.Supplements
           .FirstOrDefaultAsync(m => m.Name == supplement.Name || m.Id == supplement.Id);

            if (existSUpplement != null)
                throw new Exception($"Supplement [{supplement.Name}] already exists");

            await m_appDbContext.AddAsync(supplement);
            await m_appDbContext.SaveChangesAsync();
            return supplement;
        }

        //--------------------------------------------------
        // Update Supplement Async
        //--------------------------------------------------
        public async Task<Supplement> UpdateSupplementAsync(Supplement supplement)
        {
            var existSUpplement = await m_appDbContext.Supplements
             .FirstOrDefaultAsync(m => m.Id == supplement.Id);
            if(existSUpplement == null)
                throw new Exception($"Supplement [{supplement.Name}] does not exist");

            existSUpplement.Name = supplement.Name;
            existSUpplement.Dose = supplement.Dose;
            existSUpplement.Description = supplement.Description;
            existSUpplement.IsEssential = supplement.IsEssential;
            await m_appDbContext.SaveChangesAsync();

            return existSUpplement;
        }

        //--------------------------------------------------
        // Get Delete Supplemet
        //--------------------------------------------------
        public async Task<bool> DeleteSupplemet(int id)
        {
            var existSUpplement = await m_appDbContext.Supplements
          .FirstOrDefaultAsync(m => m.Id == id);

            if (existSUpplement == null)
                return false;

            m_appDbContext.Remove(existSUpplement);
            await m_appDbContext.SaveChangesAsync();
            return true;

        }

    }
}
