using MedSphere.DAL.Repositories._Generic;
using MedSphere.DAL.Entities.Medicines;

namespace MedSphere.DAL.Repositories.Medicines
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        
        public Task<Medicine?> GetByIdAsync (int id, bool withDeleted, CancellationToken cancellationToken );

    }
}
