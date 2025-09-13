using MedSphere.DAL.Entities.Medicine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.DAL.Configurations.Medicine;
public class MedicineIngredientConfig : IEntityTypeConfiguration<MedicineIngredient>
{
    public void Configure(EntityTypeBuilder<MedicineIngredient> builder)
    {
        builder.HasKey(mi => new { mi.MedicineId, mi.IngredientId });
    }
}
