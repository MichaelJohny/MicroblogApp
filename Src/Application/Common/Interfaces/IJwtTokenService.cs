using System.Security.Claims;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}