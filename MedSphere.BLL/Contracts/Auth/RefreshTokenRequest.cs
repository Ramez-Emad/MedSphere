using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Contracts.Auth;

public record RefreshTokenRequest
    (string token, string refreshToken);