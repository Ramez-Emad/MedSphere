namespace MedSphere.DAL.Entities.Medicines;
public class MedicineImage : AuditableEntity
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = default!;
    public bool IsPrimary { get; set; }

    public int MedicineId { get; set; }
    public Medicine Medicine { get; set; } = default!;
}
