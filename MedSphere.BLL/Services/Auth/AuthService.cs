using Azure.Core;
using Mapster;
using MedSphere.BLL.Abstractions;
using MedSphere.BLL.Contracts.Auth;
using MedSphere.BLL.Errors.Auth;
using MedSphere.BLL.Services.Auth.Jwt;
using MedSphere.DAL.Entities.Auth;
using MedSphere.DAL.Repositories.RoleClaims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace MedSphere.BLL.Services.Auth;

public class AuthService(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IRoleClaimRepository _roleClaimRepository,
    IJwtProvider _jwtProvider,
    ILogger<AuthService> _logger
    ) : IAuthService
{

    private readonly int _refreshTokenExpiryDays = 14;

    #region Login

    public async Task<Result<AuthLoginResponse>> LoginAsync(AuthLoginRequest request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Failure<AuthLoginResponse>(AuthErrors.InvalidCredentials);

        var result =await  _signInManager.CheckPasswordSignInAsync(user, request.Password, true);


        if(result.Succeeded)
        {
            var roles =  await _userManager.GetRolesAsync(user);

            var permissions = await _roleClaimRepository.GetClaimsByRolesNameAsync(roles);


            var (token , expiresIn) = _jwtProvider.GenerateJwtToken(user ,roles , permissions);

            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays),
            };

            user.RefreshTokens.Add(refreshToken);

            await _userManager.UpdateAsync(user);
 

            var response = new AuthLoginResponse(user.FirstName , user.LastName , token, expiresIn , refreshToken.Token , refreshToken.ExpiresOn);

            return Result.Success(response);
        }

        var error = result.IsNotAllowed
                   ? AuthErrors.EmailNotConfirmed
                   : result.IsLockedOut
                   ? AuthErrors.LockedUser
                   : AuthErrors.InvalidCredentials;

        return Result.Failure<AuthLoginResponse>(error);
    }

    #endregion

    #region Email Confirmation

    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var applicationUser = await _userManager.FindByIdAsync(request.UserId);

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

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<Result<AuthLoginResponse>> GetRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {

        if (_jwtProvider.ValidateToken(request.token) is not { } userId)
            return Result.Failure<AuthLoginResponse>(AuthErrors.InvalidJwtToken);

        if (await _userManager.FindByIdAsync(userId) is not { } user)
            return Result.Failure<AuthLoginResponse>(AuthErrors.InvalidJwtToken);

        if( user.LockoutEnd > DateTime.UtcNow)
            return Result.Failure<AuthLoginResponse>(AuthErrors.LockedUser);

        if (user.RefreshTokens.SingleOrDefault(x => x.Token == request.refreshToken && x.IsActive) is not { } userRefreshToken)
            return Result.Failure<AuthLoginResponse>(AuthErrors.InvalidRefreshToken);


        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var roles = await _userManager.GetRolesAsync(user);

        var permissions = await _roleClaimRepository.GetClaimsByRolesNameAsync(roles);

        var (newToken, expiresIn) = _jwtProvider.GenerateJwtToken(user , roles,permissions);
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await _userManager.UpdateAsync(user);


        var response = new AuthLoginResponse(
        
            FirstName: user.FirstName,
            LastName: user.LastName,
            Token: newToken,
            ExpiresIn: expiresIn * 60,
            RefreshToken: newRefreshToken,
            RefreshTokenExpiryTime: refreshTokenExpiration
        );

        return Result.Success(response);

    }

    public async Task<Result> RevokeRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
       
        if (_jwtProvider.ValidateToken(request.token) is not { } userId)
            return Result.Failure(AuthErrors.InvalidJwtToken);

        if (await _userManager.FindByIdAsync(userId) is not { } user)
            return Result.Failure(AuthErrors.InvalidJwtToken);

        if (user.RefreshTokens.SingleOrDefault(x => x.Token == request.refreshToken && x.IsActive) is not { } userRefreshToken)
            return Result.Failure(AuthErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return Result.Success();

    }
}
