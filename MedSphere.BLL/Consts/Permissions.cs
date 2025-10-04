using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Consts;

public static class Permissions
{
    public static string Type { get; } = "permissions";

    public const string GetIngredients = "Ingredients:read";
    public const string AddIngredients = "Ingredients:add";
    public const string UpdateIngredients = "Ingredients:update";
    public const string DeleteIngredients = "Ingredients:delete";


    public const string GetMedicines = "Medicines:read";
    public const string AddMedicines = "Medicines:add";
    public const string UpdateMedicines = "Medicines:update";
    public const string DeleteMedicines = "Medicines:delete";

    public static IList<string?> GetAllPermissions() =>
        typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList();
}