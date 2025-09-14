using MedSphere.DAL.Entities.Medicines;
using Microsoft.EntityFrameworkCore;

namespace MedSphere.DAL.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<MedicineIngredient> MedicineIngredients { get; set; }
    public DbSet<MedicineImage> MedicineImages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
