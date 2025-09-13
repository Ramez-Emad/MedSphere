using MedSphere.DAL.Data;
using MedSphere.DAL.Entities.MedicineEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedSphere.DAL.Repositories.MedicineRepo
{
    public class MedicineRepository(AppDbContext _dbContext) : IMedicineRepository
    {
        public async Task AddMedicineAsync(Medicine entity)
        {
            await _dbContext.Medicines.AddAsync(entity);
        }

        public void DeleteMedicine(Medicine entity)
        {
            entity.IsDeleted = true;
            _dbContext.Medicines.Update(entity);
        }

        public async Task<IEnumerable<Medicine>> GetAllMedicinesAsync(
                                                                bool withTracking = false,
                                                                Expression<Func<Medicine, bool>>? filter = null)
        {
            IQueryable<Medicine> query = _dbContext.Medicines.Where(m => !m.IsDeleted);

            if (filter is not null)
                query = query.Where(filter);

            if (!withTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Medicine?> GetMedicineAsync(Expression<Func<Medicine, bool>> filter)
        {
            return await _dbContext.Medicines
                                   .Where(filter)
                                   .FirstOrDefaultAsync(m => !m.IsDeleted);
        }

        public void UpdateMedicine(Medicine entity)
        {
            _dbContext.Medicines.Update(entity);   
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

       
    }
}
