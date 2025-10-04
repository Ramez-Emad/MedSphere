using MedSphere.BLL.Contracts.Auth;
using MedSphere.BLL.Services.Auth;
using MedSphere.PL.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedSphere.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService _authService) : ControllerBase
{

    [HttpPut("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
      
        var result = await _authService.RevokeRefreshTokenAsync(request, cancellationToken);


        return result.IsSuccess
            ? Ok("Refresh token revoked successfully")
            : result.ToProblem();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var authResult = await _authService.GetRefreshTokenAsync(request, cancellationToken);

        return authResult.IsSuccess
            ? Ok(authResult.Value)
            : authResult.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] AuthLoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);

        return result.IsSuccess
                     ? Ok(result.Value)
                     : result.ToProblem();
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterUserAsync(registerRequest, cancellationToken);

        return result.IsSuccess
                     ? Ok()
                     : result.ToProblem();
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {

        var result = await _authService.ConfirmEmailAsync(request, cancellationToken);
        return result.IsSuccess
                     ? Ok()
                     : result.ToProblem();

    }

    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.ResendConfirmationEmailAsync(request, cancellationToken);
        return result.IsSuccess
                     ? Ok()
                     : result.ToProblem();
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
    {

        var result = await _authService.ForgetPasswordAsync(request);
        return result.IsSuccess
                     ? Ok()
                     : result.ToProblem();

    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _authService.ResetPasswordAsync(request);
        return result.IsSuccess
                     ? Ok()
                     : result.ToProblem();
    }
}
