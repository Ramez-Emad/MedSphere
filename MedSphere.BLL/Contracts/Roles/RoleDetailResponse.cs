using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Contracts.Roles;

public record RoleDetailResponse(
    string Id,
    string Name,
    IEnumerable<string> Permissions
);