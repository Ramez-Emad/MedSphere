using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Services.Auth;

public interface IAuthService
{
    Task<Result> RegisterUserAsync(RegisterRequest registerRequest ,CancellationToken cancellationToken);

    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request , CancellationToken cancellationToken);

    Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request, CancellationToken cancellationToken);

    Task<Result> ForgetPasswordAsync(ForgetPasswordRequest request);

    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
}
