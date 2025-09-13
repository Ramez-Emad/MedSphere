namespace MedSphere.DAL.Entities.MedicineEntities;
public class Ingredient : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<MedicineIngredient> MedicineIngredients { get; set; } = new HashSet<MedicineIngredient>();
}
