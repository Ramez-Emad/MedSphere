using Mapster;
using MedSphere.BLL.Contracts.Medicines;

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
