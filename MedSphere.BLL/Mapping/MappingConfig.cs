using Mapster;
using MedSphere.BLL.Contracts.Auth;
using MedSphere.BLL.Contracts.Medicines;
using MedSphere.DAL.Entities.Auth;

namespace MedSphere.BLL.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MedicineRequest, MedicineResponse>()
                  .Ignore(dest => dest.Ingredients);

            config.NewConfig<RegisterRequest , ApplicationUser>()
                .Map(dest => dest.UserName, src => src.Email);
        }
    }
}
