using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedSphere.DAL.Entities.Medicines;

public class Medicine : AuditableEntity
{
    public int Id { get; set; }

    [MaxLength(200)]
    public string Name { get; set; } = default!;

    [MaxLength(50)]
    public string DosageForm { get; set; } = default!;

    [MaxLength(100)]
    public string Category { get; set; } = default!;

    [MaxLength(150)]
    public string FactoryName { get; set; } = default!;

    [MaxLength(100)]
    public string BatchNumber { get; set; } = default!;

    public DateTime ProductionDate { get; set; }

    public int ShelfLifeMonths { get; set; }

    [NotMapped]
    public DateTime ExpirationDate => ProductionDate.AddMonths(ShelfLifeMonths);

    public bool PrescriptionRequired { get; set; }

    [MaxLength(300)]
    public string? StorageConditions { get; set; }

    public int StockQuantity { get; set; }

    [Precision(10, 2)]
    public decimal Price { get; set; }

    [Precision(5, 2)]
    public decimal DiscountPercentage { get; set; }

    public string? SideEffects { get; set; }
    public string? Contraindications { get; set; }

    [MaxLength(100)]
    public string? BarcodeOrQRCode { get; set; }

    public ICollection<MedicineImage> MedicineImages { get; set; } = new HashSet<MedicineImage>();

    public ICollection<MedicineIngredient> MedicineIngredients { get; set; } = new HashSet<MedicineIngredient>();
}
