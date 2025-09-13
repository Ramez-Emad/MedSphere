using MedSphere.DAL.Entities.MedicineEntities;
using System.Linq.Expressions;

namespace MedSphere.DAL.Repositories.MedicineRepo
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetAllMedicinesAsync (bool WithTracking = false, Expression<Func<Medicine, bool>> filter = null!);
        
        Task<Medicine?> GetMedicineAsync (Expression<Func<Medicine, bool>> filter);
        
        Task AddMedicineAsync (Medicine entity);
        
        void UpdateMedicine (Medicine entity);
        
        void DeleteMedicine (Medicine entity);
        
        Task<int> SaveChangesAsync();

    }
}
