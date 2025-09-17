using MedSphere.DAL.Entities;
using MedSphere.DAL.Entities.Medicines;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace MedSphere.DAL.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : DbContext(options)
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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            //var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //        var userId = httpContextAccessor.HttpContext?.User?
            //.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (entry.State == EntityState.Added)
            {
               // entry.Property(x => x.CreatedById).CurrentValue = userId!;
            }
            else if (entry.State == EntityState.Modified)
            {
               // entry.Property(x => x.UpdatedById).CurrentValue = userId;
                entry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
