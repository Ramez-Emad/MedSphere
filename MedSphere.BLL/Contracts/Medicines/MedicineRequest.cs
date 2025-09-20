using MedSphere.BLL.Contracts.Ingredients;
using MedSphere.BLL.Contracts.MedicineIngredients;
using MedSphere.DAL.Entities.Medicines;

namespace MedSphere.BLL.Contracts.Medicines
{
    public record MedicineRequest(
    string Name,
    string DosageForm,
    string Category,
    string FactoryName,
    string BatchNumber,
    DateTime ProductionDate,
    int ShelfLifeMonths,
    bool PrescriptionRequired,
    string? StorageConditions,
    int StockQuantity,
    decimal Price,
    decimal DiscountPercentage,
    string? SideEffects,
    string? Contraindications,
    string? BarcodeOrQRCode,
    List<MedicineIngredientRequest> Ingredients
    );

}
