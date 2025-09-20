using MedSphere.DAL.Data;
using MedSphere.DAL.Entities.Medicines;
using MedSphere.DAL.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Repositories.Ingredients;
public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
{
    private readonly AppDbContext _appDbContext;

    public IngredientRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<bool> IsIngredientNameExists(string name, CancellationToken cancellationToken = default)
    {
        return _appDbContext.Ingredients.AnyAsync(i => i.Name == name, cancellationToken);
    }
}
