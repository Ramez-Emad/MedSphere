using MedSphere.DAL.Data;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;
using Microsoft.EntityFrameworkCore;

namespace MedSphere.DAL.Repositories.Medicines
{
    public class MedicineRepository : GenericRepository<Medicine> , IMedicineRepository
    {
        private AppDbContext _dbContext;
        public MedicineRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        
        public  async Task<Medicine?> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Medicines
                                                    .Include(m => m.MedicineIngredients.Where(mi => !mi.IsDeleted))
                                                    .Where(m => m.Id == id)
                                                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (entity is null || (!withDeleted && entity.IsDeleted))
                return null;

            return entity;
        }

    }
}
