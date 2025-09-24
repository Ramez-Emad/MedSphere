using MedSphere.DAL.Data;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;
using Microsoft.EntityFrameworkCore;

namespace MedSphere.DAL.Repositories.Ingredients;
public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
{
    private readonly AppDbContext _appDbContext;

    public IngredientRepository(AppDbContext appDbContext) : base(appDbContext) => _appDbContext = appDbContext;

    public async Task<int?> IsIngredientNameExists(string name, CancellationToken cancellationToken = default)
       => await _appDbContext.Ingredients.Where(i => i.Name == name).Select(i => i.Id).FirstOrDefaultAsync(cancellationToken);

}
