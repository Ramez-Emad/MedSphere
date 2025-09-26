using Azure.Core;
using Mapster;
using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Contracts.Auth;
using MedSphere.BLL.Errors.Auth;
using MedSphere.DAL.Entities.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MedSphere.BLL.Services.Auth;

public class AuthService(
    UserManager<ApplicationUser> _userManager,
    ILogger<AuthService> _logger
    ) : IAuthService
{

    #region Email Confirmation

    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
         var applicationUser = await  _userManager.FindByIdAsync(request.UserId);

        if (applicationUser == null)
            return Result.Failure(AuthErrors.UserNotFound);

        var code = request.Token;

        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }
        catch (FormatException)
        {
            return Result.Failure(AuthErrors.InvalidCode);
        }

        var result = await _userManager.ConfirmEmailAsync(applicationUser, code);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure(
                new Error(error.Code,
                           error.Description,
                           StatusCodes.Status400BadRequest
                   )
            );
        }

        return Result.Success();
    }

    public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } applicationUser)
            return Result.Success();

        if (applicationUser.EmailConfirmed)
            return Result.Failure(AuthErrors.DuplicatedConfirmation);

        var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
        confirmationCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationCode));

        // Send email with this code

        _logger.LogInformation("Resent confirmation code to user {Email}. UserId {userId} Confirmation code: {code}", request.Email, applicationUser.Id, confirmationCode);

        return Result.Success();
    }

    #endregion

    #region Register

    public async Task<Result> RegisterUserAsync(RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        // Check if user already exists

        if (await _userManager.FindByEmailAsync(registerRequest.Email) is { })
            return Result.Failure(AuthErrors.DuplicatedEmail);

        // Create new user

        var applicationUser = registerRequest.Adapt<ApplicationUser>();
        var createResult = await _userManager.CreateAsync(applicationUser, registerRequest.Password);

        if (!createResult.Succeeded)
        {
            var error = createResult.Errors.First();
            return Result.Failure(
                new Error(error.Code,
                           error.Description,
                           StatusCodes.Status400BadRequest
                   )
            );
        }


        // Generate confirmation token and send email

        var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);

        confirmationCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationCode));

        // Send email with this code

        _logger.LogInformation("User {Email} registered successfully.UserId {userId} Confirmation code: {code}", registerRequest.Email, applicationUser.Id, confirmationCode);

        return Result.Success();
    }

    #endregion

    #region Forget Password

    public async Task<Result> ForgetPasswordAsync(ForgetPasswordRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } applicationUser)
            return Result.Success();

        if (!applicationUser.EmailConfirmed)
            return Result.Failure(AuthErrors.EmailNotConfirmed);

        var code = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        // Send email with this code

        _logger.LogInformation("Sent password reset code to user {Email}. UserId {userId} Reset code: {code}", request.Email, applicationUser.Id, code);
        
        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.EmailConfirmed)
            return Result.Failure(AuthErrors.InvalidCode);

        var code = request.Token;
        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }
        catch (FormatException)
        {
            return Result.Failure(AuthErrors.InvalidCode);
        }

        var result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure(
                new Error(error.Code,
                           error.Description,
                           StatusCodes.Status400BadRequest
                   )
            );
        }

        return Result.Success();
    }

    #endregion
}
