using MedSphere.DAL.Data;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;

namespace MedSphere.DAL.Repositories.Medicines
{
    public class MedicineRepository(AppDbContext _dbContext) : GenericRepository<Medicine> (_dbContext), IMedicineRepository
    {
       
    }
}
