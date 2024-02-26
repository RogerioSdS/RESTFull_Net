using System.Security.Claims;

namespace RestWithASPNETUdemy.Services
{
    public interface ITokenInterface
    {
        string GenerateAcessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
