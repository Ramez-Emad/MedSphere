using FluentValidation;

namespace MedSphere.BLL.Contracts.Medicines
{
    public class MedicineValidator : AbstractValidator<MedicineRequest>
    {
        public MedicineValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(5)
                .MaximumLength(100);


            RuleFor(x=>x.DosageForm)
                .NotEmpty().WithMessage("DosageForm is required")
                .MaximumLength(100);

            RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required")
            .MaximumLength(100);

            RuleFor(x => x.FactoryName)
            .NotEmpty().WithMessage("FactoryName is required")
            .MaximumLength(100);

            RuleFor(x => x.ProductionDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Production date cannot be in the future");
           
            
            RuleFor(x => x.ShelfLifeMonths)
                .Must(value => value > 0)
                .WithMessage("Shelf life must be greater than 0");


            RuleFor(x => x.StorageConditions)
                .MaximumLength(300)
                .WithMessage("Storage Conditions life Must be smaller than 300");

            RuleFor(x => x.SideEffects)
                .MaximumLength(100)
                .WithMessage("Side effects description must not exceed 100 characters.");

            RuleFor(x => x.Contraindications)
                .MaximumLength(100)
                .WithMessage("Contraindications description must not exceed 100 characters.");

            RuleFor(x => x.BarcodeOrQRCode)
                .MaximumLength(100)
                .WithMessage("Barcode or QR code must not exceed 100 characters.");



            RuleFor(x => x.StockQuantity)
                .Must(value => value >= 0)
                .WithMessage("Stock Quantity must be Positive number ");


            RuleFor(x => x.Price)
                .Must(value => value >= 0)
                .WithMessage("Price must be Positive number ");


            RuleFor(x => x.DiscountPercentage)
                .InclusiveBetween(0, 100)
                .WithMessage("Shelf life must be Between(0, 100)");

            RuleFor(x => x.Ingredients)
                .NotEmpty()
                .WithMessage("Ingredients list cannot be empty.")
                .Must( x => x.Distinct().Count() == x.Count)
                .WithMessage("Ingredients list contains duplicate items.");

        }
    }
}
