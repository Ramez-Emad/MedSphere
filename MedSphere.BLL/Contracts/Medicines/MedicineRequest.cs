using MedSphere.BLL.Contracts.MedicineIngredients;

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
