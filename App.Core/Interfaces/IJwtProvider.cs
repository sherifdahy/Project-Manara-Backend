using App.Core.Entities.Identity;

namespace App.Core.Interfaces;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
    public string? ValidateToken(string token);
}
