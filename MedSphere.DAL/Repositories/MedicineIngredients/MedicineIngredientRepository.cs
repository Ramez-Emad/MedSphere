using MedSphere.DAL.Data;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;
using Microsoft.EntityFrameworkCore;

namespace MedSphere.DAL.Repositories.MedicineIngredients;
public class MedicineIngredientRepository : GenericRepository<MedicineIngredient>, IMedicineIngredientRepository
{
    private readonly AppDbContext _appDbContext;

    public MedicineIngredientRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<MedicineIngredient>> GetAllAsync(int mId, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.MedicineIngredients
            .Where(mi => mi.MedicineId == mId)
            .ToListAsync(cancellationToken);
    }
}
