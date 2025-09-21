
using MedSphere.DAL.Entities.Medicines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedSphere.DAL.Configurations.Medicine;
public class MedicineIngredientConfig : IEntityTypeConfiguration<MedicineIngredient>
{
    public void Configure(EntityTypeBuilder<MedicineIngredient> builder)
    {
        builder.HasKey(mi => new { mi.MedicineId, mi.IngredientId });
    }
}
