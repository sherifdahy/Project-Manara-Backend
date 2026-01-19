using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Responses.Authentications;

public record AuthenticationResponse(
    int Id,
    string? Email,
    string FirstName,
    string LastName,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration,
    int? UniversityId,
    int? FacultyId

);
