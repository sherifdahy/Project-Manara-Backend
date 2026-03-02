using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Constants;

public static class RegexPatterns
{
    public const string Password = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";
    public const string NationalId = @"^\d{14}$";
    public const string EgyptianPhoneNumber = @"^01[0-2,5]\d{8}$";
}
