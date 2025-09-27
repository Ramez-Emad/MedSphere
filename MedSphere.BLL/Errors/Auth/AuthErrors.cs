using MedSphere.BLL.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.BLL.Errors.Auth;

public class AuthErrors
{
    public static readonly Error DuplicatedEmail =
      new("User.DuplicatedEmail", "Another user with the same email is already exists", StatusCodes.Status409Conflict);

    public static readonly Error UserNotFound =
      new("User.UserNotFound", "User is not found", StatusCodes.Status404NotFound);

    public static readonly Error InvalidCode =
      new("User.InvalidCode", "Invalid code", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedConfirmation =
      new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);

    public static readonly Error EmailNotConfirmed =
       new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);



}
