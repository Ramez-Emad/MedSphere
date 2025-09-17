using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Contracts.Ingredients;
public record IngredientResponse
(
    int Id,
    string Name
);