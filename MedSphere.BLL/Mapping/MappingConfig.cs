using Mapster;
using MedSphere.BLL.Contracts.MedicineIngredients;
using MedSphere.BLL.Contracts.Medicines;
using MedSphere.DAL.Entities.Medicines;

namespace MedSphere.BLL.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MedicineRequest, MedicineResponse>()
                  .Ignore(dest => dest.Ingredients);
        }
    }
}
