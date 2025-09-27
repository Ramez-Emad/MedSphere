using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSphere.Shared.Const;

public static class RegexPatterns
{
    /// <summary>
    /// Represents a regular expression pattern used to validate passwords that meet specific complexity requirements.
    /// </summary>
    /// <remarks>
    ///     The pattern enforces the following rules:
    ///             - the password must be at least 8 characters long
    ///             - Contain at least one digit, one lowercase letter, one uppercase letter,
    ///               and one special character from the set [!@#$%^&*()\[]{}\-_+=~`|:;"'<>,./?].
    ///     This constant can be used with regular expression validation methods to ensure password strength in authentication scenarios.
    /// </remarks>
    public const string Password = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";
}
