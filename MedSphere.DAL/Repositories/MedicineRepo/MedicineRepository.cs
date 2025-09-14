using MedSphere.DAL.Data;
using MedSphere.DAL.Entities.MedicineEntities;
using MedSphere.DAL.Repositories._GenericRepo;

namespace MedSphere.DAL.Repositories.MedicineRepo
{
    public class MedicineRepository(AppDbContext _dbContext) : GenericRepository<Medicine> (_dbContext),  IMedicineRepository
    {
       
    }
}
