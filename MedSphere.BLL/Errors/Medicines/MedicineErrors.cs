using MedSphere.BLL.Abstractions;
using Microsoft.AspNetCore.Http;

namespace MedSphere.BLL.Errors.Medicines
{
    public class MedicineErrors
    {
        public static readonly Error MedicineNotFound =
        new("Medicine.NotFound", "No Medicine was found with the given ID", StatusCodes.Status404NotFound);

    }
}
