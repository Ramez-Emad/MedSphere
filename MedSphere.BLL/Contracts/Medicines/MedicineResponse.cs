namespace MedSphere.BLL.Contracts.Medicines
{
    public class MedicineResponse

    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public string DosageForm { get; set; } = default!;


        public string Category { get; set; } = default!;

        public string FactoryName { get; set; } = default!;


        public string BatchNumber { get; set; } = default!;

        public DateTime ProductionDate { get; set; }

        public int ShelfLifeMonths { get; set; }


        public bool PrescriptionRequired { get; set; }

        public string? StorageConditions { get; set; }

        public int StockQuantity { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountPercentage { get; set; }

        public string? SideEffects { get; set; }
        public string? Contraindications { get; set; }

        public string? BarcodeOrQRCode { get; set; }

        public string CreatedById { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? UpdatedById { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
