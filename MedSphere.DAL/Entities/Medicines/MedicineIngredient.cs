namespace MedSphere.DAL.Entities.Medicines;
public class MedicineIngredient : AuditableEntity
{
    public int MedicineId { get; set; }
    public Medicine Medicine { get; set; } = default!;
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = default!;
    public int? StrengthMg { get; set; }
}
