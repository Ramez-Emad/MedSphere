using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Contracts.Auth;

public record RegisterRequest(
    string Email,
    string FirstName,
    string LastName,
    string Password
    );
