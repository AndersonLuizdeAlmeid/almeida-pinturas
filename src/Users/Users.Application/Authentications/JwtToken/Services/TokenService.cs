using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Users.Application.Authentications.JwtToken.Services;
public class TokenService
{
    public static string GenerateCustomToken(long id, bool isActive)
    {
        var claims = new List<Claim>
        {
            new("userID", id.ToString()),
            new("isAuthenticated", isActive.ToString())
        };

        var key = new SymmetricSecurityKey(Convert.FromBase64String(Key.Secret!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(16),
            signingCredentials: creds,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static TokenDto DecryptToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Key.Secret!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = false, // Ou true, se quiser rejeitar tokens expirados
            ClockSkew = TimeSpan.Zero
        };

        var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);
        var jwtToken = validatedToken as JwtSecurityToken;

        TokenDto tokenDto = new TokenDto
        {
            ExpirationDate = jwtToken != null ? jwtToken.ValidTo : DateTime.MinValue,
            IsAuthenticated = principal.Claims.Any(c => c.Type == "isAuthenticated" && bool.Parse(c.Value))
        };

        return tokenDto;
    }
}