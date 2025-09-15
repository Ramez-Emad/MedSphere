using MedSphere.BLL.Contracts.Ingredients;
using MedSphere.BLL.Contracts.Medicines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Services.Ingredients;
public interface IIngredientService
{
    Task<IEnumerable<IngredientResponse>> GetAllAsync(bool WithTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default);

    Task<IngredientResponse?> GetByIdAsync(int id, bool withDeleted = false, CancellationToken cancellationToken = default);

    Task<IngredientResponse> AddAsync(IngredientRequest entity, CancellationToken cancellationToken = default);

    Task<int> Update(int id, IngredientRequest entity, CancellationToken cancellationToken = default);

    Task<bool> Delete(int id, CancellationToken cancellationToken = default);



}
